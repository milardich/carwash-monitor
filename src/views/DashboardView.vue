<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
import BoxCard from '@/components/BoxCard.vue';
import ResourceChartCard from '@/components/ResourceChartCard.vue'
import { useStationStore } from '@/stores/stationStore'
import { useResourceStore } from '@/stores/resourceStore';
import BoxPopup from '@/components/BoxPopup.vue'
import { getChartData } from '@/api/resources.api';
import { computed, onMounted, onBeforeUnmount, onUnmounted, onBeforeMount } from 'vue';
import { type Station, getAllStations } from '@/api/station.api'
import { ref } from 'vue';
import { type ResourceConsumption } from '@/api/resources.api';
import { useBoxStore } from '@/stores/boxStore';

const stationStore = useStationStore();
const resourceStore = useResourceStore();
const boxStore = useBoxStore();
const resourceConsumptions = ref<ResourceConsumption[]>([]);
var intervalId: number;

onMounted(async () => {

    stationStore.stations = await getAllStations();
    if (stationStore.selectedStation === null && stationStore.stations.length > 0) {
        stationStore.selectedStation = stationStore.stations[0];
    }
    let stationId = stationStore?.selectedStation!.id;
    let selectedStation = stationStore?.selectedStation;

    resourceStore.pgTimeInterval = "7 days";
    resourceStore.resourceConsumptions = await getChartData(
        stationId, resourceStore.pgTimeInterval.toString()
    );
    resourceConsumptions.value = resourceStore.resourceConsumptions;

    if (selectedStation === null)
        throw new Error('No station selected');
    else
        boxStore.setSelectedBox(selectedStation.boxInfos[0]);


    // // Update chart data every 5 seconds
    // intervalId = window.setInterval(() => {
    //     if (stationStore.selectedStation != null) {
    //         resourceStore.setChartDataByStationId(selectedStation.id);
    //     }
    // }, 30000);
});

onBeforeUnmount(() => {
    clearInterval(intervalId);
});

const waterData = computed(() => {
    return resourceStore.resourceConsumptions.map(
        consumption => consumption.totalWaterConsumption
    );
});

const waxData = computed(() => {
    return resourceStore.resourceConsumptions.map(
        consumption => consumption.totalWaxConsumption
    );
});

const detergentData = computed(() => {
    return resourceStore.resourceConsumptions.map(
        consumption => consumption.totalDetergentConsumption
    );
});

const labels = computed(() => {
    return resourceStore.resourceConsumptions.map(
        consumption => consumption.washCycleDate
    );
});
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
                <div class="text-3xl">Resource consumption (last 7 days)</div>
                <!-- {{ waterData }} -->
                <ResourceChartCard :labels="labels" :data="waterData" :resource-label="'Water [L]'" />

                <ResourceChartCard :labels="labels" :data="waxData" :resource-label="'Wax [L]'" />

                <ResourceChartCard :labels="labels" :data="detergentData" :resource-label="'Detergent [L]'" />
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
