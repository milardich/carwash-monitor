<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
import UnitCard from '@/components/UnitCard.vue';
import ResourceChartCard from '@/components/ResourceChartCard.vue'
import { stationStore } from '@/stores/stationStore'
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
                    <!-- TODO: fetch card data from backend -->
                    <UnitCard state="AVAILABLE" />
                    <UnitCard state="IN USE" />
                    <UnitCard state="IN USE" />
                    <UnitCard state="AVAILABLE" />
                    <UnitCard state="INACTIVE" />
                    <UnitCard state="INACTIVE" />
                    <UnitCard state="AVAILABLE" />

                    {{ console.log(stationStore.selectedStation?.units) }}
                </div>
            </div>

        </div>
    </div>


</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { type Station, getAllStations } from '@/api/station.api'

export default defineComponent({
    async created() {
        const [error, stations] = await getAllStations();
        if (error) console.error(error);
        else stationStore.stations = stations as Station[];
        console.log(stationStore.stations);
    },
    methods: {},
});
</script>