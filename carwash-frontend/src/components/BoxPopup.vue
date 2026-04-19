<script setup lang="ts">
import { useBoxStore } from '@/stores/boxStore';
import { computed } from 'vue';
import { useStationStore } from '@/stores/stationStore';

const boxStore = useBoxStore();
const stationStore = useStationStore();

const emptyTrayButtonVisible = computed(() => {
    const amount = boxStore.selectedBox?.coinTrayAmount ?? 0
    const limit = boxStore.selectedBox?.coinTrayLimit ?? 0
    return amount >= (limit - 50) && limit > 0
})
</script>

<template>
    <!-- Main modal -->
    <div v-if="boxStore.boxPopupOpen" id="default-modal" tabindex="-1"
        class="fixed inset-0 z-50 flex items-center justify-center p-2 sm:p-4 bg-black/50 backdrop-blur-sm overflow-y-auto">
        <!-- Close when clicking the backdrop -->
        <div class="absolute inset-0" @click="boxStore.toggleBoxPopup()" aria-hidden="true"></div>

        <!-- Modal content -->
        <div
            class="relative w-full max-w-5xl max-h-[95vh] flex flex-col bg-white rounded-2xl shadow-2xl modal-bg-color border-custom-blue overflow-hidden">
            <!-- Modal header -->
            <div class="flex flex-wrap items-center gap-3 p-4 sm:p-5 border-b border-gray-200">
                <h3 class="text-xl sm:text-2xl font-semibold text-black mr-auto">
                    Box #{{ boxStore?.selectedBox?.number }} <span class="text-gray-500 font-normal">- Today</span>
                </h3>

                <!-- Alert Maintenance Worker button -->
                <button type="button"
                    class="inline-flex items-center justify-center gap-2 px-3 py-2 rounded-lg bg-yellow-500 text-white text-sm font-medium hover:bg-yellow-600 transition-colors"
                    @click="boxStore.selectedBox?.status !== 'MAINTENANCE' ? boxStore.setSelectedBoxStatus('MAINTENANCE') : boxStore.setSelectedBoxStatus('INACTIVE')">
                    <template v-if="boxStore.selectedBox?.status !== 'MAINTENANCE'">
                        Alert maintenance worker
                    </template>
                    <template v-else>
                        Finish the maintenance
                    </template>
                </button>

                <button v-if="emptyTrayButtonVisible" type="button"
                    class="inline-flex items-center justify-center gap-2 px-3 py-2 rounded-lg bg-yellow-500 text-white text-sm font-medium hover:bg-yellow-600 transition-colors"
                    @click="boxStore.emptyTray(boxStore?.selectedBox?.id)">
                    Empty Tray
                </button>

                <button type="button"
                    class="text-gray-400 hover:bg-gray-100 hover:text-gray-900 rounded-lg w-9 h-9 inline-flex justify-center items-center focus:outline-none focus:ring-2 focus:ring-indigo-500"
                    aria-label="Close modal" @click="boxStore.toggleBoxPopup()">
                    <svg class="w-4 h-4" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none"
                        viewBox="0 0 14 14">
                        <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                            d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6" />
                    </svg>
                </button>
            </div>

            <template v-if="boxStore?.selectedBox?.status !== 'MAINTENANCE'">
                <!-- Scrollable body -->
                <div v-if="boxStore?.selectedBox" class="flex-1 overflow-y-auto p-4 sm:p-5 space-y-5">
                    <!-- Stat cards -->
                    <div class="grid gap-3 grid-cols-2 sm:grid-cols-3 lg:grid-cols-5">
                        <div
                            class="rounded-xl p-3 sm:p-4 shadow-md content-small-container-color border border-gray-100">
                            <div class="text-xs uppercase tracking-wide text-gray-500">Wash cycles</div>
                            <div class="text-lg sm:text-xl font-semibold mt-1">
                                {{ boxStore.selectedBox.washCycleCount }}
                            </div>
                        </div>
                        <div
                            class="rounded-xl p-3 sm:p-4 shadow-md content-small-container-color border border-gray-100">
                            <div class="text-xs uppercase tracking-wide text-gray-500">Coin tray</div>
                            <div class="text-lg sm:text-xl font-semibold mt-1">
                                {{ boxStore.selectedBox.coinTrayAmount ?? 0 }} /
                                {{ boxStore?.selectedBox.coinTrayLimit ?? 0 }}
                            </div>
                        </div>
                        <div
                            class="rounded-xl p-3 sm:p-4 shadow-md content-small-container-color border border-gray-100">
                            <div class="text-xs uppercase tracking-wide text-gray-500">Water</div>
                            <div class="text-lg sm:text-xl font-semibold mt-1">
                                {{ boxStore.selectedBox.totalWaterConsumption }} L
                            </div>
                        </div>
                        <div
                            class="rounded-xl p-3 sm:p-4 shadow-md content-small-container-color border border-gray-100">
                            <div class="text-xs uppercase tracking-wide text-gray-500">Detergent</div>
                            <div class="text-lg sm:text-xl font-semibold mt-1">
                                {{ boxStore.selectedBox.totalDetergentConsumption }} L
                            </div>
                        </div>
                        <div
                            class="rounded-xl p-3 sm:p-4 shadow-md content-small-container-color border border-gray-100 col-span-2 sm:col-span-1">
                            <div class="text-xs uppercase tracking-wide text-gray-500">Wax</div>
                            <div class="text-lg sm:text-xl font-semibold mt-1">
                                {{ boxStore.selectedBox.totalWaxConsumption }} L
                            </div>
                        </div>
                    </div>

                    <!-- Wash cycles table -->
                    <div v-if="boxStore.selectedBox.washCycles.length > 0"
                        class="rounded-lg shadow-md overflow-hidden border border-gray-100">
                        <div class="overflow-x-auto max-h-[60vh] overflow-y-auto">
                            <table class="w-full text-sm text-left rtl:text-right text-gray-500">
                                <thead class="text-xs text-gray-700 uppercase bg-gray-50 sticky top-0">
                                    <tr>
                                        <th scope="col" class="px-4 sm:px-6 py-3">Wash Cycle ID</th>
                                        <th scope="col" class="px-4 sm:px-6 py-3">Coin amount</th>
                                        <th scope="col" class="px-4 sm:px-6 py-3">Water [L]</th>
                                        <th scope="col" class="px-4 sm:px-6 py-3">Detergent [L]</th>
                                        <th scope="col" class="px-4 sm:px-6 py-3">Wax [L]</th>
                                        <th scope="col" class="px-4 sm:px-6 py-3">Time</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="washCycle in boxStore.selectedBox.washCycles" :key="washCycle.id"
                                        class="odd:bg-white even:bg-gray-50 border-b last:border-b-0">
                                        <td class="px-4 sm:px-6 py-3">{{ washCycle.id }}</td>
                                        <td class="px-4 sm:px-6 py-3">{{ washCycle.coinAmount }}</td>
                                        <td class="px-4 sm:px-6 py-3">{{ washCycle.waterConsumption }}</td>
                                        <td class="px-4 sm:px-6 py-3">{{ washCycle.detergentConsumption }}</td>
                                        <td class="px-4 sm:px-6 py-3">{{ washCycle.waxConsumption }}</td>
                                        <td class="px-4 sm:px-6 py-3 whitespace-nowrap">
                                            {{ new Date(washCycle.dateCreated).toLocaleTimeString("hr-HR") }}
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div v-else class="text-gray-500 italic text-sm">
                        This box does not have any wash cycles.
                    </div>
                </div>
                <div v-else class="p-6 text-gray-500">
                    Loading unit info...
                </div>

                <!-- Modal footer / status selector -->
                <div v-if="stationStore?.selectedStation && boxStore?.selectedBox"
                    class="grid gap-3 grid-cols-1 sm:grid-cols-3 p-4 sm:p-5 border-t border-gray-200 bg-white">
                    <label for="AVAILABLE"
                        class="cursor-pointer rounded-xl px-4 py-2 text-center font-semibold shadow-md transition-all duration-200 available-bg-color text-white hover:brightness-110"
                        :class="{ 'ring-2 ring-offset-2 ring-green-500 scale-[1.02]': boxStore.selectedBox?.status === 'AVAILABLE' }">
                        <input type="radio" id="AVAILABLE" value="AVAILABLE" v-model="boxStore.selectedBox.status"
                            class="hidden" @click="boxStore.setSelectedBoxStatus('AVAILABLE')" />
                        AVAILABLE
                    </label>

                    <label for="INACTIVE"
                        class="cursor-pointer rounded-xl px-4 py-2 text-center font-semibold shadow-md transition-all duration-200 inactive-bg-color text-white hover:brightness-110"
                        :class="{ 'ring-2 ring-offset-2 ring-gray-500 scale-[1.02]': boxStore.selectedBox?.status === 'INACTIVE' }">
                        <input type="radio" id="INACTIVE" value="INACTIVE" v-model="boxStore.selectedBox.status"
                            class="hidden" @click="boxStore.setSelectedBoxStatus('INACTIVE')" />
                        INACTIVE
                    </label>

                    <label for="IN_USE"
                        class="cursor-pointer rounded-xl px-4 py-2 text-center font-semibold shadow-md transition-all duration-200 in-use-bg-color text-white hover:brightness-110"
                        :class="{ 'ring-2 ring-offset-2 ring-blue-500 scale-[1.02]': boxStore.selectedBox?.status === 'IN_USE' }">
                        <input type="radio" id="IN_USE" value="IN_USE" v-model="boxStore.selectedBox.status"
                            class="hidden" @click="boxStore.setSelectedBoxStatus('IN_USE')" />
                        IN USE
                    </label>
                </div>
            </template>
            <template v-else>
                <div class="p-8 text-center text-gray-600 italic flex-1 flex items-center justify-center">
                    This box is undergoing maintenance.
                </div>
            </template>
        </div>
    </div>
</template>
