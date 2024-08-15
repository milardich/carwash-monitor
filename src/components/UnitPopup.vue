<script setup lang="ts">
import { useUnitStore } from '@/stores/unitPopup';
import { ref, watch } from 'vue';
import { changeUnitStatus } from '@/api/unit.api';
import { useStationStore } from '@/stores/stationStore';

const unitStore = useUnitStore();
const stationStore = useStationStore();
const picked = ref(unitStore.selectedUnit?.status);

watch(
    () => unitStore.selectedUnit,
    (unit) => {
        picked.value = unit?.status;
    }
);

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
                <div v-if="unitStore.selectedUnitInfo">
                    <div class="mt-6">
                        <div>Wash count: {{ unitStore.selectedUnitInfo.washCycleCount }}</div>
                        <div>Coin amount: {{ unitStore.selectedUnitInfo.totalCoinAmount }}</div>
                        <div>Water consumption: {{ unitStore.selectedUnitInfo.totalWaterConsumption }}</div>
                        <div>Detergent consumption: {{ unitStore.selectedUnitInfo.totalDetergentConsumption }}</div>
                        <div>Wax consumption: {{ unitStore.selectedUnitInfo.totalWaxConsumption }}</div>
                    </div>
                </div>
                <div v-else>
                    Loading unit info...
                </div>



                <!-- Modal footer -->
                <div class="mt-6 grid grid-cols-3" v-if="stationStore.selectedStation && unitStore.selectedUnit">
                    <div>
                        <input type="radio" id="AVAILABLE" value="AVAILABLE" v-model="picked" @click="
                            changeUnitStatus(stationStore.selectedStation?.stationId, unitStore.selectedUnit?.unitId, 'AVAILABLE');
                        unitStore.setUnitStatus('AVAILABLE')" />
                        <label for="AVAILABLE">AVAILABLE</label>
                    </div>

                    <div>
                        <input type="radio" id="INACTIVE" value="INACTIVE" v-model="picked" @click="changeUnitStatus(stationStore.selectedStation?.stationId, unitStore.selectedUnit?.unitId, 'INACTIVE');
                        unitStore.setUnitStatus('INACTIVE')" />
                        <label for="INACTIVE">INACTIVE</label>
                    </div>

                    <div>
                        <input type="radio" id="IN_USE" value="IN_USE" v-model="picked" @click="changeUnitStatus(stationStore.selectedStation?.stationId, unitStore.selectedUnit?.unitId, 'IN_USE');
                        unitStore.setUnitStatus('IN_USE')" />
                        <label for="IN_USE">IN_USE</label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>