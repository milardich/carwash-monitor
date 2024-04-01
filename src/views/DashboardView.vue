<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
import UnitCard from '@/components/UnitCard.vue';
import ResourceChartCard from '@/components/ResourceChartCard.vue'
</script>



<template>
    <div class="h-full">
        <div>
            <StationDropdown :stations="stations" />
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
                </div>
            </div>

        </div>
    </div>


</template>

<script lang="ts">
import { getAllStations, getStation, type Station } from "@/api/station.api";
import { defineComponent } from 'vue';

export default defineComponent({
    data() {
        return {
            stations: [] as Station[],
        }
    },
    async created() {
        const [error, stations] = await getAllStations();
        if (error) console.error(error);
        else this.stations = stations as Station[];
        console.log(this.stations);
    },
    methods: {},
});
</script>