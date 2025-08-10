<script lang="ts" setup>
import { computed, onMounted, onBeforeUnmount } from 'vue';
import type { Box } from '@/api/box.api';
import { useBoxStore } from '@/stores/boxStore';

const props = defineProps<{ box: Box }>();
const boxStore = useBoxStore();
const boxStateLabel = computed(() => props.box.status);
const backgroundColorCssClass = computed(() => {
    switch (props.box.status) {
        case 'AVAILABLE': return 'available-bg-color';
        case 'IN_USE': return 'in-use-bg-color';
        case 'INACTIVE': return 'inactive-bg-color';
        default: return 'bg-yellow-warning';
    }
});

onMounted(async () => {
    await boxStore.getBox(props.box.id);
    boxStore.startAutoRefresh(props.box.id);
});

onBeforeUnmount(() => {
    boxStore.stopAutoRefresh();
});

</script>

<template>
    <div
        class="box-border h-48 w-56 grid grid-cols-1 rounded-2xl border-1 border-black shadow-xl p-4 content-between content-small-container-color">
        <!-- Header row: box number + settings button -->
        <div class="flex">
            <div class="text-3xl">#{{ box.number }}</div>
            <div class="ml-auto justify-end">
                <button @click="
                    boxStore.setSelectedBox(box);
                boxStore.getBox(box.id);
                boxStore.toggleBoxPopup();
                ">
                    <img src="@/assets/settings-svgrepo-com.svg" alt="Settings" class="card-button-icon-small" />
                </button>
            </div>
        </div>

        <!-- Box info -->
        <div class="mt-2">
            <div class="grid grid-cols-1 text-sm">
                <div v-if="box">
                    <div>
                        Washes today:
                        <span class="font-bold">{{ box.washCycleCount }}</span>
                    </div>
                    <div>
                        Coin tray:
                        <span class="font-bold">{{ box.totalCoinAmount }}</span>
                    </div>
                </div>
                <div v-else>
                    <div>Loading info...</div>
                </div>
            </div>
        </div>

        <!-- Status -->
        <div class="border-1 border-black rounded-xl text-center p-1" :class="backgroundColorCssClass">
            {{ boxStateLabel }}
        </div>
    </div>
</template>
