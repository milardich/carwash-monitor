<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
import UnitCard from '@/components/UnitCard.vue';
import ResourceChartCard from '@/components/ResourceChartCard.vue'
import { stationStore } from '@/stores/stationStore'
import UnitPopup from '@/components/UnitPopup.vue'
import { getChartData } from '@/api/resources.api';
import { computed } from 'vue';
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
        if (stationStore.selectedStation == null)
            stationStore.selectedStation = stationStore.stations[0];
        var stationId = stationStore.selectedStation.stationId;
        resourceStore.pgTimeInterval = "24 hours";
        var pgTimeInterval = resourceStore.pgTimeInterval;
        resourceStore.resourceConsumptions = await getChartData(stationId, pgTimeInterval);
        resourceConsumptions.value = resourceStore.resourceConsumptions;
    },
    methods: {},
});


export const waterData = computed(() => {
    return resourceStore.resourceConsumptions.map(consumption => consumption.totalWaterConsumption);
});

export const waxData = computed(() => {
    return resourceStore.resourceConsumptions.map(consumption => consumption.totalWaxConsumption);
});

export const detergentData = computed(() => {
    return resourceStore.resourceConsumptions.map(consumption => consumption.totalDetergentConsumption);
});

export const labels = computed(() => {
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
