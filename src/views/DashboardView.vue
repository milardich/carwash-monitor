<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
import UnitCard from '@/components/UnitCard.vue';
import ResourceChartCard from '@/components/ResourceChartCard.vue'
import { stationStore } from '@/stores/stationStore'
import UnitPopup from '@/components/UnitPopup.vue'
import { getChartData } from '@/api/resources.api';
</script>


<script lang="ts">
import { defineComponent } from 'vue'
import { type Station, getAllStations } from '@/api/station.api'
import { resourceStore } from '@/stores/resourceStore';
import { ref } from 'vue';
import { type ResourceConsumption } from '@/api/resources.api';

const stations = ref<Station[]>([]);
const resourceConsumptions = ref<ResourceConsumption[]>([]);

export default defineComponent({
    async mounted() {
        stations.value = await getAllStations();
        stationStore.stations = stations.value;
        stationStore.selectedStation = stationStore.stations[0];
        resourceStore.resourceConsumptions = await getChartData(1, "100 days");
        resourceStore.pgTimeInterval = "100 days";
        resourceConsumptions.value = resourceStore.resourceConsumptions;
    },
    methods: {},
});

function populateWaterConsumptionData(): number[] {
    var waterData: number[] = [];
    resourceStore.resourceConsumptions.forEach((consumption) => {
        waterData.push(consumption.totalWaterConsumption);
    });
    return waterData;
}

function populateWaterConsumptionLabels(): String[] {
    var waterLabels: String[] = [];
    resourceStore.resourceConsumptions.forEach((consumption) => {
        waterLabels.push(consumption.washCycleDate);
    });
    return waterLabels;
}

function populateWaxConsumptionData(): number[] {
    var waxData: number[] = [];
    resourceStore.resourceConsumptions.forEach((consumption) => {
        waxData.push(consumption.totalWaxConsumption);
    });
    return waxData;
}

function populateWaxConsumptionLabels(): String[] {
    var waxLabels: String[] = [];
    resourceStore.resourceConsumptions.forEach((consumption) => {
        waxLabels.push(consumption.washCycleDate);
    });
    return waxLabels;
}

function populateDetergentConsumptionData(): number[] {
    var detergentData: number[] = [];
    resourceStore.resourceConsumptions.forEach((consumption) => {
        detergentData.push(consumption.totalDetergentConsumption);
    });
    return detergentData;
}

function populateDetergentConsumptionLabels(): String[] {
    var detergentLabels: String[] = [];
    resourceStore.resourceConsumptions.forEach((consumption) => {
        detergentLabels.push(consumption.washCycleDate);
    });
    return detergentLabels;
}

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

                <div v-if="resourceStore.resourceConsumptions.length > 0">

                    <ResourceChartCard :labels="populateWaterConsumptionLabels()" :data="populateWaterConsumptionData()"
                        :resource="'Water [L]'" />

                    <ResourceChartCard :labels="populateWaxConsumptionLabels()" :data="populateWaxConsumptionData()"
                        :resource="'Wax [L]'" />

                    <ResourceChartCard :labels="populateDetergentConsumptionLabels()"
                        :data="populateDetergentConsumptionData()" :resource="'Detergent [L]'" />
                </div>

                <div v-else>
                    Loading charts...
                </div>
            </div>

            <div class="rounded-lg overflow-y-auto p-6 h-full bg-violet-ultralight shadow-lg">
                <div class="text-3xl">Units</div>

                <div class="grid grid-cols-3 gap-4 mt-6">
                    <span v-if="stationStore.selectedStation?.units"
                        v-for="unit in stationStore.selectedStation?.units ">
                        <UnitCard :unit="unit" />
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
