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
                Section 1
            </div>
            <div class="rounded-lg border-2 border-black overflow-y-auto p-6 h-full">
                section 2
            </div>
        </div>
    </div>

</template>