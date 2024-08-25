<script setup lang="ts">
import { useUnitStore } from '@/stores/unitPopup';
import { onBeforeUnmount, onMounted, ref, watch } from 'vue';
import { changeUnitStatus, getUnitInfo, type UnitInfo } from '@/api/unit.api';
import { useStationStore } from '@/stores/stationStore';
import { strDateTime, strDateTimeMidnight } from '@/util/dateTimeUtils';

const unitStore = useUnitStore();
const stationStore = useStationStore();
const picked = ref(unitStore.selectedUnit?.status);
const unitInfo = ref<UnitInfo | null>();

var intervalId: number;

onMounted(async () => {
    if (unitStore.selectedUnitInfo) {
        unitInfo.value = unitStore.selectedUnitInfo;
    }

    intervalId = window.setInterval(async () => {
        var currentDate = new Date();
        unitStore.dateTo = strDateTime(currentDate);
        unitStore.dateFrom = strDateTimeMidnight(currentDate);
        try {
            unitInfo.value = await getUnitInfo(
                unitStore.dateFrom,
                unitStore.dateTo,
                stationStore.selectedStation?.stationId,
                unitStore.selectedUnit?.unitId
            );
        } catch (error) {
            throw (error);
        }
        // TODO: fetch wash cycles, put them in a list, and display in table down below
    }, 5000);
});

onBeforeUnmount(() => {
    clearInterval(intervalId);
});

watch(
    () => unitStore.selectedUnit,
    (unit) => {
        picked.value = unit?.status;
    }
);

watch(
    () => unitStore.selectedUnit?.status,
    (newStatus) => {
        picked.value = newStatus
    }
);

watch(
    () => unitStore.selectedUnitInfo,
    (newUnitInfo) => {
        unitInfo.value = newUnitInfo;
    }
);


</script>

<template>
    <!-- Main modal -->
    <div id="default-modal" :class="{ 'hidden': !unitStore.unitPopupOpen }" tabindex="-1"
        class="flex justify-center items-center h-screen overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 backdrop-blur-sm z-50 bg-transparent-black">
        <!-- Popup -->
        <div class="absolute p-2 w-full max-w-5xl">
            <!-- Modal content -->
            <div class="relative bg-white rounded-lg shadow-2xl modal-bg-color p-4 border-custom-blue">
                <!-- Modal header -->
                <div class="flex items-center justify-between rounded-t dark:border-gray-600">
                    <h3 class="text-2xl font-semibold text-black">
                        Unit #{{ unitStore.selectedUnit?.unitId }} - Today
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
                <div v-if="unitInfo">
                    <div class="mt-6">
                        <div>
                            <div class="font-semibold table-footer font-black flex m-4 rounded-lg">
                                <div scope="row" class="px-6 py-3 text-base">Wash cycle count:
                                    {{ unitInfo.washCycleCount }}</div>
                                <div class="px-6 py-3"> Coin amount: {{ unitInfo.totalCoinAmount }}
                                </div>
                                <div class="px-6 py-3"> Water: {{ unitInfo.totalWaterConsumption }} L
                                </div>
                                <div class="px-6 py-3"> Detergent: {{
                                    unitInfo.totalDetergentConsumption }} L
                                </div>
                                <div class="px-6 py-3"> Wax: {{ unitInfo.totalWaxConsumption }} L
                                </div>
                            </div>
                        </div>



                        <div class="m-4 shadow-2xl max-h-96 overflow-y-scroll ">
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
                                        <tr class="odd:bg-white even:bg-gray-50 border-b">
                                            <td class="px-6 py-4">
                                                543
                                            </td>
                                            <td class="px-6 py-4">
                                                2
                                            </td>
                                            <td class="px-6 py-4">
                                                40 L
                                            </td>
                                            <td class="px-6 py-4">
                                                4 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2024-08-15
                                            </td>
                                        </tr>
                                        <tr class="odd:bg-white even:bg-gray-50 border-b">
                                            <td class="px-6 py-4">
                                                543
                                            </td>
                                            <td class="px-6 py-4">
                                                2
                                            </td>
                                            <td class="px-6 py-4">
                                                40 L
                                            </td>
                                            <td class="px-6 py-4">
                                                4 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2024-08-15
                                            </td>
                                        </tr>
                                        <tr class="odd:bg-white even:bg-gray-50 border-b">
                                            <td class="px-6 py-4">
                                                543
                                            </td>
                                            <td class="px-6 py-4">
                                                2
                                            </td>
                                            <td class="px-6 py-4">
                                                40 L
                                            </td>
                                            <td class="px-6 py-4">
                                                4 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2024-08-15
                                            </td>
                                        </tr>
                                        <tr class="odd:bg-white even:bg-gray-50 border-b">
                                            <td class="px-6 py-4">
                                                543
                                            </td>
                                            <td class="px-6 py-4">
                                                2
                                            </td>
                                            <td class="px-6 py-4">
                                                40 L
                                            </td>
                                            <td class="px-6 py-4">
                                                4 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2024-08-15
                                            </td>
                                        </tr>
                                        <tr class="odd:bg-white even:bg-gray-50 border-b">
                                            <td class="px-6 py-4">
                                                543
                                            </td>
                                            <td class="px-6 py-4">
                                                2
                                            </td>
                                            <td class="px-6 py-4">
                                                40 L
                                            </td>
                                            <td class="px-6 py-4">
                                                4 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2024-08-15
                                            </td>
                                        </tr>
                                        <tr class="odd:bg-white even:bg-gray-50 border-b">
                                            <td class="px-6 py-4">
                                                543
                                            </td>
                                            <td class="px-6 py-4">
                                                2
                                            </td>
                                            <td class="px-6 py-4">
                                                40 L
                                            </td>
                                            <td class="px-6 py-4">
                                                4 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2024-08-15
                                            </td>
                                        </tr>
                                        <tr class="odd:bg-white even:bg-gray-50 border-b">
                                            <td class="px-6 py-4">
                                                543
                                            </td>
                                            <td class="px-6 py-4">
                                                2
                                            </td>
                                            <td class="px-6 py-4">
                                                40 L
                                            </td>
                                            <td class="px-6 py-4">
                                                4 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2 L
                                            </td>
                                            <td class="px-6 py-4">
                                                2024-08-15
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div v-else>
                    Loading unit info...
                </div>



                <!-- Modal footer -->
                <div class="mt-6 grid grid-cols-3" v-if="stationStore.selectedStation && unitStore.selectedUnit">
                    <div class="available-bg-color rounded-xl p-1 m-2 text-center">
                        <input type="radio" id="AVAILABLE" value="AVAILABLE" v-model="picked" @click="
                            changeUnitStatus(stationStore.selectedStation?.stationId, unitStore.selectedUnit?.unitId, 'AVAILABLE');
                        unitStore.setUnitStatus('AVAILABLE')" />
                        <label for="AVAILABLE">AVAILABLE</label>
                    </div>

                    <div class="inactive-bg-color rounded-xl p-1 m-2 text-center">
                        <input type="radio" id="INACTIVE" value="INACTIVE" v-model="picked" @click="changeUnitStatus(stationStore.selectedStation?.stationId, unitStore.selectedUnit?.unitId, 'INACTIVE');
                        unitStore.setUnitStatus('INACTIVE')" />
                        <label for="INACTIVE">INACTIVE</label>
                    </div>

                    <div class="in-use-bg-color rounded-xl p-1 m-2 text-center">
                        <input type="radio" id="IN_USE" value="IN_USE" v-model="picked" @click="changeUnitStatus(stationStore.selectedStation?.stationId, unitStore.selectedUnit?.unitId, 'IN_USE');
                        unitStore.setUnitStatus('IN_USE')" />
                        <label for="IN_USE">IN_USE</label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>