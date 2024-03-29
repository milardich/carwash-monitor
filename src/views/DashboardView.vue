<script setup lang="ts">
import StationDropdown from '@/components/StationDropdown.vue';
</script>


<template>
    <div class="h-full">
        <div>
            <StationDropdown />
        </div>

        <div class="grid grid-cols-2 gap-4 border-2 border-green h-percent-90 mt-10">
            <div class="rounded-lg border-2 border-black overflow-y-auto p-6 h-full">
                <div class="text-lg">Resource consumption</div>

                <!-- TODO: make this a component -->
                <div>
                    <div class="rounded-lg border-2 border-black h-64 mt-6">
                        Resource consumption graph 1
                    </div>
                    <div class="rounded-lg border-2 border-black h-64 mt-6">
                        Resource consumption graph 1
                    </div>
                    <div class="rounded-lg border-2 border-black h-64 mt-6">
                        Resource consumption graph 1
                    </div>
                    <div class="rounded-lg border-2 border-black h-64 mt-6">
                        Resource consumption graph 1
                    </div>
                </div>
            </div>

            <div class="rounded-lg border-2 border-black overflow-y-auto p-6 h-full">
                <div class="text-lg">Units</div>

                <!-- TODO: make this a component -->
                <div class="grid grid-cols-3 gap-4 mt-6">
                    <div class="box-border h-48 w-56 rounded-lg border-2 border-black">
                        Unit 1
                    </div>
                    <div class="box-border h-48 w-56 rounded-lg border-2 border-black">
                        Unit 1
                    </div>
                    <div class="box-border h-48 w-56 rounded-lg border-2 border-black">
                        Unit 1
                    </div>
                    <div class="box-border h-48 w-56 rounded-lg border-2 border-black">
                        Unit 1
                    </div>
                    <div class="box-border h-48 w-56 rounded-lg border-2 border-black">
                        Unit 1
                    </div>
                    <div class="box-border h-48 w-56 rounded-lg border-2 border-black">
                        Unit 1
                    </div>
                    <div class="box-border h-48 w-56 rounded-lg border-2 border-black">
                        Unit 1
                    </div>
                </div>

            </div>
        </div>
    </div>

</template>