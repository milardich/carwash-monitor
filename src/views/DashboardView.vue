<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
import BoxCard from '@/components/BoxCard.vue';
import ResourceChartCard from '@/components/ResourceChartCard.vue'
import { useStationStore } from '@/stores/stationStore'
import { useResourceStore } from '@/stores/resourceStore';
import BoxPopup from '@/components/BoxPopup.vue'
import { onBeforeUnmount, onMounted } from 'vue';
import { useBoxStore } from '@/stores/boxStore';
import { watch } from 'vue';

const stationStore = useStationStore();
const resourceStore = useResourceStore();
const boxStore = useBoxStore();

let refreshIntervalId: number | null = null;

async function init() {
    await stationStore.loadStations();
    const station = stationStore.selectedStation;
    if (!station) return;

    await resourceStore.loadResourceConsumptions(station.id);

    if (station.boxes?.length && !boxStore.selectedBox) {
        boxStore.setSelectedBox(station.boxes[0]);
    }
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
    if (newStation.boxes?.length) {
        boxStore.setSelectedBox(newStation.boxes[0]);
    }
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
        <div>
            <span v-if="stationStore.hasStations">
                <StationDropdown />
            </span>
            <span v-else>
                Loading stations...
            </span>
        </div>

        <div class="grid grid-cols-2 gap-4 h-percent-90 mt-5">
            <div class="rounded-lg overflow-y-auto p-6 h-full  content-background-container-color shadow-md">
                <div class="text-3xl">Resource consumption (Today)</div>
                <!-- {{ waterData }} -->
                <ResourceChartCard :labels="resourceStore.labels" :data="resourceStore.waterData"
                    :resource-label="'Water [L]'" />

                <ResourceChartCard :labels="resourceStore.labels" :data="resourceStore.waxData"
                    :resource-label="'Wax [L]'" />

                <ResourceChartCard :labels="resourceStore.labels" :data="resourceStore.detergentData"
                    :resource-label="'Detergent [L]'" />
            </div>

            <div class="rounded-lg overflow-y-auto p-6 h-full content-background-container-color shadow-md">
                <div class="text-3xl">Boxes</div>

                <template v-if="stationStore.selectedStation">
                    <template
                        v-if="stationStore.selectedStation.boxes && stationStore.selectedStation.boxes.length > 0">
                        <div class="grid grid-cols-3 gap-4 mt-6">
                            <span v-for="box in stationStore.selectedStation.boxes" :key="box.id">
                                <Suspense>
                                    <BoxCard :box="box" />
                                </Suspense>
                            </span>

                            <!-- POPUP -->
                            <Suspense>
                                <BoxPopup />
                            </Suspense>
                        </div>
                    </template>
                    <template v-else>
                        <div class="mt-4 text-lg font-semibold text-gray-600">
                            This station has no boxes.
                        </div>
                    </template>
                </template>

                <template v-else>
                    Loading boxes...
                </template>
            </div>


        </div>
    </div>


</template>
