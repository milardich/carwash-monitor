<script setup lang="ts">
import { useUnitStore } from '@/stores/unitPopup';
import StationDropdown from './StationDropdown.vue';
import FilterDropdown from './FilterDropdown.vue';
import { getUnit, getUnitInfo, type UnitInfo } from '@/api/unit.api';
import { useStationStore } from '@/stores/stationStore';
import { onBeforeUpdate, onMounted, onUpdated, reactive } from 'vue';

const unitStore = useUnitStore();

</script>

<template>
    <!-- Main modal -->
    <div id="default-modal" :class="{ 'hidden': !unitStore.unitPopupOpen }" tabindex="-1"
        class="flex justify-center items-center h-screen overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 backdrop-blur-sm z-50 bg-transparent-black">
        <!-- Popup -->
        <div class="absolute p-2 w-full max-w-2xl max-h-full">
            <!-- Modal content -->
            <div class="relative bg-white rounded-lg shadow dark:bg-gray-700 bg-white-light p-4">
                <!-- Modal header -->
                <div class="flex items-center justify-between rounded-t dark:border-gray-600">
                    <h3 class="text-2xl font-semibold dark:text-white">
                        Unit #{{ unitStore.selectedUnit?.unitId }}
                    </h3>
                    <button type="button"
                        class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
                        data-modal-hide="default-modal" @click="unitStore.toggleUnitPopup()">
                        <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none"
                            viewBox="0 0 14 14">
                            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6" />
                        </svg>
                        <span class="sr-only">Close modal</span>
                    </button>
                </div>


                <!-- Modal body -->
                <div class="flex mt-6">
                    <div>
                        Filter by:
                    </div>
                    <div class="ml-6">
                        <FilterDropdown />
                    </div>
                </div>

                <div v-if="unitStore.selectedUnitInfo">
                    <div class="mt-6">
                        <div>Number of washes: {{ unitStore.selectedUnitInfo.washCycleCount }}</div> <!-- TODO: -->
                        <div>Coin tray: {{ unitStore.selectedUnitInfo.totalCoinAmount }}</div>
                        <div>Water consumption: {{ unitStore.selectedUnitInfo.totalWaterConsumption }}</div>
                        <div>Detergent consumption: {{ unitStore.selectedUnitInfo.totalDetergentConsumption }}</div>
                        <div>Wax consumption: {{ unitStore.selectedUnitInfo.totalWaxConsumption }}</div>
                    </div>
                </div>
                <div v-else>
                    Loading unit info...
                </div>



                <!-- Modal footer -->
                <div class="mt-6">
                    TODO: change status button
                </div>
            </div>
        </div>
    </div>
</template>