<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
import BoxCard from '@/components/BoxCard.vue';
import ResourceChartCard from '@/components/ResourceChartCard.vue'
import { useStationStore } from '@/stores/stationStore'
import { useResourceStore } from '@/stores/resourceStore';
import BoxPopup from '@/components/BoxPopup.vue'
import { computed, onMounted } from 'vue';
import { useBoxStore } from '@/stores/boxStore';
import { watch } from 'vue';

const stationStore = useStationStore();
const resourceStore = useResourceStore();
const boxStore = useBoxStore();

async function init() {
    await stationStore.loadStations();
    const station = stationStore.selectedStation;
    if (!station) return;

    await resourceStore.loadResourceConsumptions(station.id);

    if (station.boxInfos?.length && !boxStore.selectedBox) {
        boxStore.setSelectedBox(station.boxInfos[0]);
    }
}

watch(() => stationStore.selectedStation, async (newStation) => {
    if (!newStation) return;
    await resourceStore.loadResourceConsumptions(newStation.id);
    if (newStation.boxInfos?.length) {
        boxStore.setSelectedBox(newStation.boxInfos[0]);
    }
});

onMounted(() => {
    init();
});

// TODO: implement auto fetching
</script>


<template>
    <div class="h-full">
        <div>
            <span v-if="stationStore.stations.length > 0">
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

                <div class="grid grid-cols-3 gap-4 mt-6">
                    <span v-if="stationStore?.selectedStation?.boxInfos"
                        v-for="box in stationStore?.selectedStation?.boxInfos">
                        <Suspense>
                            <BoxCard :box="box" />
                        </Suspense>
                    </span>
                    <span v-else>
                        Loading units...
                    </span>

                    <!-- testing this -->
                    <Suspense>
                        <BoxPopup />
                    </Suspense>
                </div>
            </div>

        </div>
    </div>


</template>
