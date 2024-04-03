<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
import UnitCard from '@/components/UnitCard.vue';
import ResourceChartCard from '@/components/ResourceChartCard.vue'
import { stationStore } from '@/stores/stationStore'
</script>


<script lang="ts">
import { defineComponent } from 'vue'
import { type Station, getAllStations } from '@/api/station.api'

export default defineComponent({
    async mounted() {
        const [error, stations] = await getAllStations();
        if (error) console.error(error);
        else stationStore.stations = stations as Station[];
        stationStore.selectedStation = stationStore.stations[0];
    },
    methods: {},
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

                <div>
                    <!-- TODO: fetch card data from backend -->
                    <ResourceChartCard />
                    <ResourceChartCard />
                    <ResourceChartCard />
                    <ResourceChartCard />
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


                </div>
            </div>

        </div>
    </div>


</template>
