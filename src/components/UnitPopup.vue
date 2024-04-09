<script setup lang="ts">
import { unitPopup, toggleUnitPopup } from '@/stores/unitPopup';

</script>

<template>
    <!-- Main modal -->
    <div id="default-modal" :class="{ 'hidden': !unitPopup.isOpen }" tabindex="-1"
        class="flex justify-center items-center h-screen overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 backdrop-blur-sm z-50 bg-transparent-black">
        <!-- Popup -->
        <div class="absolute p-2 w-full max-w-2xl max-h-full">
            <!-- Modal content -->
            <div class="relative bg-white rounded-lg shadow dark:bg-gray-700 bg-white-light p-4">
                <!-- Modal header -->
                <div class="flex items-center justify-between rounded-t dark:border-gray-600">
                    <h3 class="text-2xl font-semibold dark:text-white">
                        Unit #{{ unitPopup.selectedUnit?.unitId }}
                    </h3>
                    <button type="button"
                        class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
                        data-modal-hide="default-modal" @click="toggleUnitPopup">
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
                        Today <!-- TODO: make this a dropdown -->
                    </div>
                </div>

                <div class="mt-6">
                    <div>Number of washes: 12</div> <!-- TODO: -->
                    <div>Coin tray: {{ unitPopup.selectedUnit?.coinTrayAmount }}</div>
                    <div>Water consumption: {{ unitPopup.selectedUnit?.totalWaterConsumption }}</div>
                    <div>Detergent consumption: {{ unitPopup.selectedUnit?.totalDetergentConsumption }}</div>
                    <div>Wax consumption: {{ unitPopup.selectedUnit?.totalWaxConsumption }}</div>
                </div>


                <!-- Modal footer -->
                <div class="mt-6">
                    TODO: change status button
                </div>
            </div>
        </div>
    </div>
</template>