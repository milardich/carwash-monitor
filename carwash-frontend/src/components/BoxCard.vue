<script lang="ts" setup>
import { computed } from 'vue';
import type { Box } from '@/api/box.api';
import { useBoxStore } from '@/stores/boxStore';

const props = defineProps<{ box: Box }>();
const boxStore = useBoxStore();
const backgroundColorCssClass = computed(() => {
    switch (props.box.status) {
        case 'AVAILABLE': return 'available-bg-color';
        case 'IN_USE': return 'in-use-bg-color';
        case 'INACTIVE': return 'inactive-bg-color';
        case 'MAINTENANCE': return 'maintenance-bg-color';
        default: return 'bg-yellow-warning';
    }
});
</script>

<template>
    <div
        class="box-border w-full h-48 flex flex-col justify-between rounded-2xl border border-black shadow-xl p-4 content-small-container-color">
        <!-- Header row: box number + settings button -->
        <div class="flex items-center">
            <div class="text-2xl sm:text-3xl">#{{ box.number }}</div>
            <button @click="boxStore.setSelectedBox(box); boxStore.toggleBoxPopup();"
                class="ml-auto p-1 rounded hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-indigo-500"
                aria-label="Open box details">
                <img src="@/assets/settings-svgrepo-com.svg" alt="Settings" class="card-button-icon-small" />
            </button>
        </div>

        <!-- Box info -->
        <div class="text-sm">
            <div>
                Washes today:
                <span class="font-bold">{{ box.washCycleCount }}</span>
            </div>
            <div>
                Coin tray:
                <span class="font-bold">{{ box.coinTrayAmount }}/{{ box.coinTrayLimit }}</span>
            </div>
        </div>

        <!-- Status -->
        <div class="border border-black rounded-xl text-center p-1 truncate" :class="backgroundColorCssClass">
            {{ box.status }}
        </div>
    </div>
</template>
