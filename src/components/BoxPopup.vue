<script setup lang="ts">
import { useBoxStore } from '@/stores/boxStore';
import { onBeforeUnmount, onMounted } from 'vue';
import { useStationStore } from '@/stores/stationStore';

const boxStore = useBoxStore();
const stationStore = useStationStore();

onMounted(() => {

});

onBeforeUnmount(() => {

});

</script>

<template>
    <!-- Main modal -->
    <div id="default-modal" :class="{ 'hidden': !boxStore.boxPopupOpen }" tabindex="-1"
        class="flex justify-center items-center h-screen overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 backdrop-blur-sm z-50 bg-transparent-black">
        <!-- Popup -->
        <div class="absolute p-2 w-full max-w-5xl">
            <!-- Modal content -->
            <div class="relative bg-white rounded-lg shadow-2xl modal-bg-color p-4 border-custom-blue">
                <!-- Modal header -->
                <div class="flex items-center justify-between rounded-t dark:border-gray-600">
                    <h3 class="text-2xl font-semibold text-black">
                        Box #{{ boxStore?.selectedBox?.number }} - Today
                    </h3>
                    <button type="button"
                        class="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
                        data-modal-hide="default-modal" @click="boxStore.toggleBoxPopup()">
                        <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none"
                            viewBox="0 0 14 14">
                            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6" />
                        </svg>
                        <span class="sr-only">Close modal</span>
                    </button>
                </div>


                <!-- Modal body -->
                <div v-if="boxStore.selectedBox">
                    <div class="mt-6">
                        <div>
                            <div class="font-semibold table-footer font-black flex m-4 rounded-lg">
                                <div scope="row" class="px-6 py-3 text-base">Wash cycle count:
                                    {{ boxStore.selectedBox.washCycleCount }}</div>
                                <div class="px-6 py-3"> Coin amount: {{ boxStore.selectedBox.totalCoinAmount }}
                                </div>
                                <div class="px-6 py-3"> Water: {{ boxStore.selectedBox.totalWaterConsumption }} L
                                </div>
                                <div class="px-6 py-3"> Detergent: {{
                                    boxStore.selectedBox.totalDetergentConsumption }} L
                                </div>
                                <div class="px-6 py-3"> Wax: {{ boxStore.selectedBox.totalWaxConsumption }} L
                                </div>
                            </div>
                        </div>



                        <div class="m-4 shadow-2xl h-96 max-h-96 overflow-y-scroll "
                            v-if="boxStore.selectedBox.washCycles.length > 0">
                            <div class="overflow-x-auto  h-max shadow-xl sm:rounded-lg">
                                <table class="w-full text-sm text-left rtl:text-right text-gray-500">
                                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 ">
                                        <tr>
                                            <th scope="col" class="px-6 py-3">
                                                Wash Cycle ID
                                            </th>
                                            <th scope="col" class="px-6 py-3">
                                                Coin amount
                                            </th>
                                            <th scope="col" class="px-6 py-3">
                                                Water
                                            </th>
                                            <th scope="col" class="px-6 py-3">
                                                Detergent
                                            </th>
                                            <th scope="col" class="px-6 py-3">
                                                Wax
                                            </th>
                                            <th scope="col" class="px-6 py-3">
                                                Date
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <template v-for="washCycle in boxStore.selectedBox.washCycles"
                                            :key="washCycle.id">
                                            <tr class="odd:bg-white even:bg-gray-50 border-b">
                                                <td class="px-6 py-4">
                                                    {{ washCycle.id }}
                                                </td>
                                                <td class="px-6 py-4">
                                                    {{ washCycle.coinAmount }}
                                                </td>
                                                <td class="px-6 py-4">
                                                    {{ washCycle.waterConsumption }}
                                                </td>
                                                <td class="px-6 py-4">
                                                    {{ washCycle.detergentConsumption }}
                                                </td>
                                                <td class="px-6 py-4">
                                                    {{ washCycle.waxConsumption }}
                                                </td>
                                                <td class="px-6 py-4">
                                                    {{ washCycle.dateCreated }}
                                                </td>
                                            </tr>
                                        </template>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div v-else> This box does not have any wash cycles. </div>
                    </div>
                </div>
                <div v-else>
                    Loading unit info...
                </div>



                <!-- Modal footer -->
                <div class="mt-6 grid grid-cols-3 gap-4" v-if="stationStore?.selectedStation && boxStore?.selectedBox">
                    <!-- ACTIVE -->
                    <label for="ACTIVE" class="cursor-pointer rounded-xl px-4 py-2 text-center font-semibold shadow-md transition-all duration-200
                        available-bg-color text-white hover:brightness-110" :class="{
                            'ring-2 ring-offset-2 ring-green-500 scale-105': boxStore.selectedBox?.status === 'ACTIVE'
                        }">
                        <input type="radio" id="ACTIVE" value="ACTIVE" v-model="boxStore.selectedBox.status"
                            class="hidden" @click="boxStore.setBoxStatus('ACTIVE')" />
                        ACTIVE
                    </label>

                    <!-- INACTIVE -->
                    <label for="INACTIVE" class="cursor-pointer rounded-xl px-4 py-2 text-center font-semibold shadow-md transition-all duration-200
                        inactive-bg-color text-white hover:brightness-110" :class="{
                            'ring-2 ring-offset-2 ring-gray-500 scale-105': boxStore.selectedBox?.status === 'INACTIVE'
                        }">
                        <input type="radio" id="INACTIVE" value="INACTIVE" v-model="boxStore.selectedBox.status"
                            class="hidden" @click="boxStore.setBoxStatus('INACTIVE')" />
                        INACTIVE
                    </label>

                    <!-- IN USE -->
                    <label for="IN_USE" class="cursor-pointer rounded-xl px-4 py-2 text-center font-semibold shadow-md transition-all duration-200
                        in-use-bg-color text-white hover:brightness-110" :class="{
                            'ring-2 ring-offset-2 ring-blue-500 scale-105': boxStore.selectedBox?.status === 'IN_USE'
                        }">
                        <input type="radio" id="IN_USE" value="IN_USE" v-model="boxStore.selectedBox.status"
                            class="hidden" @click="boxStore.setBoxStatus('IN_USE')" />
                        IN USE
                    </label>
                </div>


            </div>
        </div>
    </div>
</template>