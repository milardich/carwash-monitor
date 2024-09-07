<script lang="ts" setup>
import { getStatistics, type StationStatistics, type StatisticsHighlights, getStatisticsSummary, type StatisticsSummary } from '@/api/statistics.api';
import { getMonthName, strDateTime, strDateTimeMonthBegin } from '@/util/dateTimeUtils';
import { onMounted } from 'vue';
import { ref } from 'vue';
import { watch } from 'vue';

const statisticsHighlights = ref<StatisticsHighlights>();
const statisticsSummary = ref<StatisticsSummary>();
var currentDate = new Date();
var dateTo = strDateTime(currentDate);
var dateFrom = strDateTimeMonthBegin(currentDate);
const timezone: string = Intl.DateTimeFormat().resolvedOptions().timeZone;
var monthName = getMonthName(currentDate.getMonth());

const currencySign = import.meta.env.VITE_CARWASH_CURRENCY_SIGN;

onMounted(async () => {
    console.log(dateTo, dateFrom, timezone);
    try {
        statisticsHighlights.value = await getStatistics(dateFrom, dateTo, timezone);
        statisticsSummary.value = await getStatisticsSummary(dateFrom, dateTo, timezone);
    } catch (error) {
        throw (error);
    }
});

watch(
    () => statisticsHighlights.value,
    (newValue) => {
        statisticsHighlights.value = newValue;
    }
);

watch(
    () => statisticsSummary.value,
    (newValue) => {
        statisticsSummary.value = newValue;
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
                    <div class="flex text-sm font-medium text-gray">
                        <div>Total revenue</div>
                    </div>

                    <div class="text-3xl text-black">
                        {{ currencySign }} {{ statisticsHighlights?.totalRevenue.toFixed(2) }}
                    </div>

                    <!-- TODO: increase / decrease from previous month -->
                    <!-- <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium text-green">

                        <span>32k increase</span>

                        <svg class="w-4 h-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor"
                            aria-hidden="true">
                            <path fill-rule="evenodd"
                                d="M12 7a1 1 0 110-2h5a1 1 0 011 1v5a1 1 0 11-2 0V8.414l-4.293 4.293a1 1 0 01-1.414 0L8 10.414l-4.293 4.293a1 1 0 01-1.414-1.414l5-5a1 1 0 011.414 0L11 10.586 14.586 7H12z"
                                clip-rule="evenodd"></path>
                        </svg>
                    </div> -->
                </div>
            </div>



            <div class="relative p-6 rounded-2xl bg-white shadow-xl">
                <div class="space-y-2">
                    <div class="flex items-center space-x-2 rtl:space-x-reverse text-sm font-medium text-gray">
                        <span>Best performing station</span>
                    </div>

                    <div class="text-3xl text-black">
                        {{ statisticsHighlights?.bestStationName }}
                    </div>

                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium">

                        <span>Total revenue: {{ currencySign }} {{ statisticsHighlights?.bestStationRevenue.toFixed(2)
                            }}</span>
                    </div>
                </div>
            </div>



            <div class="relative p-6 rounded-2xl bg-white shadow-xl">
                <div class="space-y-2">
                    <div class="flex items-center space-x-2 rtl:space-x-reverse text-sm font-medium text-gray">
                        <span>Number of washes</span>
                    </div>

                    <div class="text-3xl text-black">
                        {{ statisticsHighlights?.totalWashCount }}
                    </div>


                    <!-- TODO: increase / decrease from previous month -->
                    <!-- <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium text-red">
                        <span>7% decrease</span>

                        <svg class="w-4 h-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor"
                            aria-hidden="true">
                            <path fill-rule="evenodd"
                                d="M12 13a1 1 0 100 2h5a1 1 0 001-1V9a1 1 0 10-2 0v2.586l-4.293-4.293a1 1 0 00-1.414 0L8 9.586 3.707 5.293a1 1 0 00-1.414 1.414l5 5a1 1 0 001.414 0L11 9.414 14.586 13H12z"
                                clip-rule="evenodd"></path>
                        </svg>
                    </div> -->
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
                        {{ statisticsHighlights?.totalWaterConsumption }}L
                    </div>

                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium">

                        <span>Total cost: {{ currencySign }} {{ statisticsHighlights?.totalWaterCost.toFixed(2)
                            }}</span>

                    </div>
                </div>
            </div>



            <div class="relative p-6 rounded-2xl bg-white shadow-xl">
                <div class="space-y-2">
                    <div class="flex items-center space-x-2 rtl:space-x-reverse text-sm font-medium text-gray">
                        <span>Detergent consumption</span>
                    </div>

                    <div class="text-3xl text-black">
                        {{ statisticsHighlights?.totalDetergentConsumption }}L
                    </div>

                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium">

                        <span>Total cost: {{ currencySign }} {{ statisticsHighlights?.totalDetergentCost.toFixed(2)
                            }}</span>
                    </div>
                </div>
            </div>



            <div class="relative p-6 rounded-2xl bg-white shadow-xl">
                <div class="space-y-2">
                    <div class="flex items-center space-x-2 rtl:space-x-reverse text-sm font-medium text-gray">
                        <span>Wax consumption</span>
                    </div>

                    <div class="text-3xl text-black">
                        {{ statisticsHighlights?.totalWaxConsumption.toFixed(2) }}L
                    </div>

                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium">

                        <span>Total cost: {{ currencySign }} {{ statisticsHighlights?.totalWaxCost.toFixed(2) }}</span>
                    </div>
                </div>
            </div>
        </div>


        <!-- Statistics summary table -->

        <div v-if="statisticsSummary">
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

                            </tr>
                        </thead>
                        <tbody>
                            <tr class="odd:bg-white even:bg-gray-50 border-b" v-if="statisticsSummary"
                                v-for="stationStatistics in statisticsSummary.allStationStatistics">
                                <td class="px-6 py-4">
                                    {{ stationStatistics.stationName }}
                                </td>
                                <td class="px-6 py-4">
                                    {{ currencySign }} {{ stationStatistics.revenue.toFixed(2) }}
                                </td>
                                <td class="px-6 py-4">
                                    {{ currencySign }} {{ stationStatistics.waterCost.toFixed(2) }}
                                </td>
                                <td class="px-6 py-4">
                                    {{ currencySign }} {{ stationStatistics.detergentCost.toFixed(2) }}
                                </td>
                                <td class="px-6 py-4">
                                    {{ currencySign }} {{ stationStatistics.waxCost.toFixed(2) }}
                                </td>

                            </tr>
                        </tbody>
                        <tfoot>
                            <tr class="font-semibold table-footer font-black">
                                <th scope="row" class="px-6 py-3 text-base">Total</th>
                                <td class="px-6 py-3">{{ currencySign }} {{ statisticsSummary.totalRevenue.toFixed(2) }}
                                </td>
                                <td class="px-6 py-3">{{ currencySign }} {{ statisticsSummary.totalWaterCost.toFixed(2)
                                    }}</td>
                                <td class="px-6 py-3">{{ currencySign }} {{
                                    statisticsSummary.totalDetergentCost.toFixed(2) }}</td>
                                <td class="px-6 py-3">{{ currencySign }} {{ statisticsSummary.totalWaxCost.toFixed(2) }}
                                </td>

                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>
        <div v-else>
            Loading statistics summary table...
        </div>




    </div>
</template>