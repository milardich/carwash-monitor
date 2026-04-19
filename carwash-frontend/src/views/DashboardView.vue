<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
import BoxCard from '@/components/BoxCard.vue';
import ResourceChartCard from '@/components/ResourceChartCard.vue'
import { useStationStore } from '@/stores/stationStore'
import { useResourceStore } from '@/stores/resourceStore';
import BoxPopup from '@/components/BoxPopup.vue'
import { onBeforeUnmount, onMounted } from 'vue';
import { watch } from 'vue';

const stationStore = useStationStore();
const resourceStore = useResourceStore();

let refreshIntervalId: number | null = null;

async function init() {
    await stationStore.loadStations();
    const station = stationStore.selectedStation;
    if (!station) return;

    await resourceStore.loadResourceConsumptions(station.id);
}

function startAutoRefresh() {
    stopAutoRefresh();
    refreshIntervalId = window.setInterval(async () => {
        const station = stationStore.selectedStation;
        if (station) {
            await resourceStore.loadResourceConsumptions(station.id);
        }
        await stationStore.updateSelectedStation();
    }, 5_000);
}

function stopAutoRefresh() {
    if (refreshIntervalId !== null) {
        clearInterval(refreshIntervalId);
        refreshIntervalId = null;
    }
}

watch(() => stationStore.selectedStation, async (newStation) => {
    if (!newStation) return;
    await resourceStore.loadResourceConsumptions(newStation.id);
});

onMounted(async () => {
    await init();
    startAutoRefresh();
});

onBeforeUnmount(() => {
    stopAutoRefresh();
});
</script>


<template>
    <div class="h-full">
        <div class="p-2 sm:p-4">
            <div class="flex flex-col sm:flex-row sm:items-center sm:gap-6 gap-3">
                <h1 class="text-2xl sm:text-3xl font-bold">Dashboard</h1>
                <span v-if="stationStore.hasStations">
                    <StationDropdown />
                </span>
                <span v-else class="text-gray-500">
                    Loading stations...
                </span>
            </div>
        </div>

        <div class="grid grid-cols-1 lg:grid-cols-2 gap-4 mt-4 lg:mt-5">
            <!-- Resource charts: shown second on mobile, first (left) on desktop -->
            <section
                class="order-2 lg:order-1 rounded-lg p-4 sm:p-6 content-background-container-color shadow-md lg:h-[calc(100vh-8rem)] lg:overflow-y-auto">
                <div class="text-2xl sm:text-3xl">Resource consumption (Today)</div>
                <ResourceChartCard :labels="resourceStore.labels" :data="resourceStore.waterData"
                    :resource-label="'Water [L]'" />
                <ResourceChartCard :labels="resourceStore.labels" :data="resourceStore.waxData"
                    :resource-label="'Wax [L]'" />
                <ResourceChartCard :labels="resourceStore.labels" :data="resourceStore.detergentData"
                    :resource-label="'Detergent [L]'" />
            </section>

            <!-- Boxes: shown first on mobile, second (right) on desktop -->
            <section
                class="order-1 lg:order-2 rounded-lg p-4 sm:p-6 content-background-container-color shadow-md lg:h-[calc(100vh-8rem)] lg:overflow-y-auto">
                <div class="text-2xl sm:text-3xl">Boxes</div>

                <template v-if="stationStore.selectedStation">
                    <template
                        v-if="stationStore.selectedStation.boxes && stationStore.selectedStation.boxes.length > 0">
                        <div class="grid gap-4 mt-4 sm:mt-6 grid-cols-[repeat(auto-fill,minmax(12rem,1fr))]">
                            <Suspense v-for="box in stationStore.selectedStation.boxes" :key="box.id">
                                <BoxCard :box="box" />
                            </Suspense>
                        </div>
                        <Suspense>
                            <BoxPopup />
                        </Suspense>
                    </template>
                    <template v-else>
                        <div class="mt-4 text-lg font-semibold text-gray-600">
                            This station has no boxes.
                        </div>
                    </template>
                </template>

                <template v-else>
                    <div class="mt-4 text-gray-500">Loading boxes...</div>
                </template>
            </section>
        </div>
    </div>
</template>
