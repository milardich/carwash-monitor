<script lang="ts" setup>
import { getStatistics, type Statistics } from '@/api/statistics.api';
import { getMonthName, strDateTime, strDateTimeMonthBegin } from '@/util/dateTimeUtils';
import { onMounted } from 'vue';
import { ref } from 'vue';
import { watch } from 'vue';

const statistics = ref<Statistics>();
var currentDate = new Date();
var dateTo = strDateTime(currentDate);
var dateFrom = strDateTimeMonthBegin(currentDate);
const timezone: string = Intl.DateTimeFormat().resolvedOptions().timeZone;
var monthName = getMonthName(currentDate.getMonth());

onMounted(async () => {
    console.log(dateTo, dateFrom, timezone);
    try {
        statistics.value = await getStatistics(dateFrom, dateTo, timezone);
    } catch (error) {
        throw (error);
    }
});

watch(
    () => statistics.value,
    (newValue) => {
        statistics.value = newValue;
    }
);

</script>

<template>
    <div class="h-full">
        <div class="p-4">
            <h1 class="text-3xl">Stats ({{ monthName }}, {{ currentDate.getFullYear() }})</h1>
        </div>


        <!-- General data -->

        <div class="grid gap-4 lg:gap-8 md:grid-cols-3 p-4 pt-8">
            <div class="relative p-6 rounded-2xl bg-white shadow-xl">
                <div class="space-y-2">
                    <div class="flex items-center space-x-2 rtl:space-x-reverse text-sm font-medium text-gray">
                        <span>Total revenue</span>
                    </div>

                    <div class="text-3xl text-black">
                        ${{ statistics?.totalRevenue }}
                    </div>

                    <!-- TODO: increase / decrease from previous month -->
                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium text-green">

                        <span>32k increase</span>

                        <svg class="w-4 h-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor"
                            aria-hidden="true">
                            <path fill-rule="evenodd"
                                d="M12 7a1 1 0 110-2h5a1 1 0 011 1v5a1 1 0 11-2 0V8.414l-4.293 4.293a1 1 0 01-1.414 0L8 10.414l-4.293 4.293a1 1 0 01-1.414-1.414l5-5a1 1 0 011.414 0L11 10.586 14.586 7H12z"
                                clip-rule="evenodd"></path>
                        </svg>
                    </div>
                </div>
            </div>



            <div class="relative p-6 rounded-2xl bg-white shadow-xl">
                <div class="space-y-2">
                    <div class="flex items-center space-x-2 rtl:space-x-reverse text-sm font-medium text-gray">
                        <span>Best performing station</span>
                    </div>

                    <div class="text-3xl text-black">
                        {{ statistics?.bestStationName }}
                    </div>

                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium">

                        <span>Total revenue: ${{ statistics?.bestStationRevenue }}</span>
                    </div>
                </div>
            </div>



            <div class="relative p-6 rounded-2xl bg-white shadow-xl">
                <div class="space-y-2">
                    <div class="flex items-center space-x-2 rtl:space-x-reverse text-sm font-medium text-gray">
                        <span>Number of washes</span>
                    </div>

                    <div class="text-3xl text-black">
                        {{ statistics?.totalWashCount }}
                    </div>


                    <!-- TODO: increase / decrease from previous month -->
                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium text-red">
                        <span>7% decrease</span>

                        <svg class="w-4 h-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor"
                            aria-hidden="true">
                            <path fill-rule="evenodd"
                                d="M12 13a1 1 0 100 2h5a1 1 0 001-1V9a1 1 0 10-2 0v2.586l-4.293-4.293a1 1 0 00-1.414 0L8 9.586 3.707 5.293a1 1 0 00-1.414 1.414l5 5a1 1 0 001.414 0L11 9.414 14.586 13H12z"
                                clip-rule="evenodd"></path>
                        </svg>
                    </div>
                </div>
            </div>

        </div>



        <!-- Consumptions -->

        <div class="grid gap-4 lg:gap-8 md:grid-cols-3 p-4">
            <div class="relative p-6 rounded-2xl bg-white shadow-xl">
                <div class="space-y-2">
                    <div class="flex items-center space-x-2 rtl:space-x-reverse text-sm font-medium text-gray">
                        <span>Water consumption</span>
                    </div>

                    <div class="text-3xl text-black">
                        {{ statistics?.totalWaterConsumption }}L
                    </div>

                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium">

                        <span>Total cost: ${{ statistics?.totalWaterCost }}</span>

                    </div>
                </div>
            </div>



            <div class="relative p-6 rounded-2xl bg-white shadow-xl">
                <div class="space-y-2">
                    <div class="flex items-center space-x-2 rtl:space-x-reverse text-sm font-medium text-gray">
                        <span>Detergent consumption</span>
                    </div>

                    <div class="text-3xl text-black">
                        {{ statistics?.totalDetergentConsumption }}L
                    </div>

                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium">

                        <span>Total cost: ${{ statistics?.totalDetergentCost }}</span>
                    </div>
                </div>
            </div>



            <div class="relative p-6 rounded-2xl bg-white shadow-xl">
                <div class="space-y-2">
                    <div class="flex items-center space-x-2 rtl:space-x-reverse text-sm font-medium text-gray">
                        <span>Wax consumption</span>
                    </div>

                    <div class="text-3xl text-black">
                        {{ statistics?.totalWaxConsumption }}L
                    </div>

                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium">

                        <span>Total cost: ${{ statistics?.totalWaxCost }}</span>
                    </div>
                </div>
            </div>
        </div>


        <!-- All stations stats table -->

        <div class="m-4 shadow-2xl">
            <div class="overflow-x-auto shadow-xl sm:rounded-lg">
                <table class="w-full text-sm text-left rtl:text-right text-gray-500">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50 ">
                        <tr>
                            <th scope="col" class="px-6 py-3">
                                Station name
                            </th>
                            <th scope="col" class="px-6 py-3">
                                Total revenue
                            </th>
                            <th scope="col" class="px-6 py-3">
                                Water cost
                            </th>
                            <th scope="col" class="px-6 py-3">
                                Detergent cost
                            </th>
                            <th scope="col" class="px-6 py-3">
                                Wax cost
                            </th>
                            <th scope="col" class="px-6 py-3">
                                Gross revenue
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="odd:bg-white even:bg-gray-50 border-b">
                            <td class="px-6 py-4">
                                Vinkovci-1
                            </td>
                            <td class="px-6 py-4">
                                $32,540
                            </td>
                            <td class="px-6 py-4">
                                $2,421
                            </td>
                            <td class="px-6 py-4">
                                $4,421
                            </td>
                            <td class="px-6 py-4">
                                $3,421
                            </td>
                            <td class="px-6 py-4">
                                $22,540
                            </td>
                        </tr>
                        <tr class="odd:bg-white even:bg-gray-50 border-b ">
                            <td class="px-6 py-4">
                                Vinkovci-2
                            </td>
                            <td class="px-6 py-4">
                                $32,540
                            </td>
                            <td class="px-6 py-4">
                                $2,421
                            </td>
                            <td class="px-6 py-4">
                                $4,421
                            </td>
                            <td class="px-6 py-4">
                                $3,421
                            </td>
                            <td class="px-6 py-4">
                                $22,540
                            </td>
                        </tr>
                        <tr class="odd:bg-white even:bg-gray-50 border-b">
                            <td class="px-6 py-4">
                                Osijek-2
                            </td>
                            <td class="px-6 py-4">
                                $32,540
                            </td>
                            <td class="px-6 py-4">
                                $2,421
                            </td>
                            <td class="px-6 py-4">
                                $4,421
                            </td>
                            <td class="px-6 py-4">
                                $3,421
                            </td>
                            <td class="px-6 py-4">
                                $22,540
                            </td>
                        </tr>
                        <tr class="odd:bg-white even:bg-gray-50  border-b ">
                            <td class="px-6 py-4">
                                Osijek-3
                            </td>
                            <td class="px-6 py-4">
                                $32,540
                            </td>
                            <td class="px-6 py-4">
                                $2,421
                            </td>
                            <td class="px-6 py-4">
                                $4,421
                            </td>
                            <td class="px-6 py-4">
                                $3,421
                            </td>
                            <td class="px-6 py-4">
                                $22,540
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr class="font-semibold table-footer font-black">
                            <th scope="row" class="px-6 py-3 text-base">Total</th>
                            <td class="px-6 py-3">$192,493</td>
                            <td class="px-6 py-3">$8,435</td>
                            <td class="px-6 py-3">$20,212</td>
                            <td class="px-6 py-3">$12,212</td>
                            <td class="px-6 py-3">$154,861</td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>



    </div>
</template>