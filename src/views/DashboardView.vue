<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
import UnitCard from '@/components/UnitCard.vue';
</script>


<template>
    <div class="h-full">
        <div>
            <StationDropdown />
        </div>

        <div class="grid grid-cols-2 gap-4 h-percent-90 mt-5">
            <div class="rounded-lg overflow-y-auto p-6 h-full  bg-white-medium shadow-lg">
                <div class="text-3xl">Resource consumption</div>

                <!-- TODO: make this a component -->
                <div>
                    <div class="rounded-2xl border-1 border-black h-64 mt-10 shadow-lg bg-white-light">
                        Resource consumption graph 1
                    </div>
                    <div class="rounded-2xl border-1 border-black h-64 mt-10 shadow-lg bg-white-light">
                        Resource consumption graph 1
                    </div>
                    <div class="rounded-2xl border-1 border-black h-64 mt-10 shadow-lg bg-white-light">
                        Resource consumption graph 1
                    </div>
                    <div class="rounded-2xl border-1 border-black h-64 mt-10 shadow-lg bg-white-light">
                        Resource consumption graph 1
                    </div>
                </div>
            </div>

            <div class="rounded-lg overflow-y-auto p-6 h-full bg-white-medium shadow-lg">
                <div class="text-3xl">Units</div>

                <!-- TODO: make this a component -->
                <div class="grid grid-cols-3 gap-4 mt-6">
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