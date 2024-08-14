<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
import UnitCard from '@/components/UnitCard.vue';
import ResourceChartCard from '@/components/ResourceChartCard.vue'
import { useStationStore } from '@/stores/stationStore'
import { useResourceStore } from '@/stores/resourceStore';
import UnitPopup from '@/components/UnitPopup.vue'
import { getChartData } from '@/api/resources.api';
import { computed, onMounted, onBeforeUnmount, onUnmounted, onBeforeMount } from 'vue';
import { defineComponent } from 'vue'
import { type Station, getAllStations } from '@/api/station.api'
import { ref } from 'vue';
import { type ResourceConsumption } from '@/api/resources.api';
import type { Store } from 'pinia';



const stationStore = useStationStore();
const resourceStore = useResourceStore();

const stations = ref<Station[]>([]);
const resourceConsumptions = ref<ResourceConsumption[]>([]);

var intervalId: number;

onMounted(async () => {
    stations.value = await getAllStations();
    stationStore.stations = stations.value;
    if (stationStore.selectedStation == null)
        stationStore.selectedStation = stationStore.stations[0];
    var stationId = stationStore.selectedStation.stationId;
    resourceStore.pgTimeInterval = "7 days";
    var pgTimeInterval = resourceStore.pgTimeInterval;
    resourceStore.resourceConsumptions = await getChartData(stationId, pgTimeInterval);
    resourceConsumptions.value = resourceStore.resourceConsumptions;

    intervalId = setInterval(() => {
        if (stationStore.selectedStation != null) {
            resourceStore.setChartDataByStationId(stationStore.selectedStation.stationId);
        }
    }, 5000);
});

onBeforeUnmount(() => {
    clearInterval(intervalId);
});


const waterData = computed(() => {
    return resourceStore.resourceConsumptions.map(consumption => consumption.totalWaterConsumption);
});

const waxData = computed(() => {
    return resourceStore.resourceConsumptions.map(consumption => consumption.totalWaxConsumption);
});

const detergentData = computed(() => {
    return resourceStore.resourceConsumptions.map(consumption => consumption.totalDetergentConsumption);
});

const labels = computed(() => {
    return resourceStore.resourceConsumptions.map(consumption => consumption.washCycleDate);
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
            <div class="rounded-lg overflow-y-auto p-6 h-full  bg-violet-ultralight shadow-lg">
                <div class="text-3xl">Resource consumption</div>
                <!-- {{ waterData }} -->
                <ResourceChartCard :labels="labels" :data="waterData" :resource-label="'Water [L]'" />

                <ResourceChartCard :labels="labels" :data="waxData" :resource-label="'Wax [L]'" />

                <ResourceChartCard :labels="labels" :data="detergentData" :resource-label="'Detergent [L]'" />
            </div>

            <div class="rounded-lg overflow-y-auto p-6 h-full bg-violet-ultralight shadow-lg">
                <div class="text-3xl">Units</div>

                <div class="grid grid-cols-3 gap-4 mt-6">
                    <span v-if="stationStore.selectedStation?.units"
                        v-for="unit in stationStore.selectedStation?.units ">
                        <Suspense>
                            <UnitCard :unit="unit" />
                        </Suspense>
                    </span>
                    <span v-else>
                        Loading units...
                    </span>

                    <!-- testing this -->
                    <UnitPopup />
                </div>
            </div>

        </div>
    </div>


</template>
