<script lang="ts" setup>
import { useStatisticsStore } from '@/stores/statisticsStore';
import { onMounted } from 'vue';

const statisticsStore = useStatisticsStore();
const currencySign = '€'
let currentDate: Date = new Date()

onMounted(async () => {
    statisticsStore.loadStatistics();
});

</script>

<template>
    <div class="h-full">
        <div class="p-4">
            <h1 class="text-3xl">Stats ({{ currentDate.getMonth() + 1 }}, {{ currentDate.getFullYear() }})</h1>
        </div>


        <!-- General data -->

        <div class="grid gap-4 lg:gap-8 md:grid-cols-3 p-4 pt-8">
            <div class="relative p-6 rounded-2xl bg-white shadow-xl">
                <div class="space-y-2">
                    <div class="flex text-sm font-medium text-gray">
                        <div>Total revenue</div>
                    </div>

                    <div class="text-3xl text-black">
                        {{ currencySign }} {{ statisticsStore.statisticsHighlights?.totalRevenue.toFixed(2) }}
                    </div>
                </div>
            </div>



            <div class="relative p-6 rounded-2xl bg-white shadow-xl">
                <div class="space-y-2">
                    <div class="flex items-center space-x-2 rtl:space-x-reverse text-sm font-medium text-gray">
                        <span>Best performing station</span>
                    </div>

                    <div class="text-3xl text-black">
                        {{ statisticsStore.statisticsHighlights?.bestStationName }}
                    </div>

                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium">

                        <span>Total revenue: {{ currencySign }} {{
                            statisticsStore.statisticsHighlights?.bestStationRevenue.toFixed(2)
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
                        {{ statisticsStore.statisticsHighlights?.totalWashCount }}
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
                        {{ statisticsStore.statisticsHighlights?.totalWaterConsumption }}L
                    </div>

                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium">

                        <span>Total cost: {{ currencySign }} {{
                            statisticsStore.statisticsHighlights?.totalWaterCost.toFixed(2)
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
                        {{ statisticsStore.statisticsHighlights?.totalDetergentConsumption }}L
                    </div>

                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium">

                        <span>Total cost: {{ currencySign }} {{
                            statisticsStore.statisticsHighlights?.totalDetergentCost.toFixed(2)
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
                        {{ statisticsStore.statisticsHighlights?.totalWaxConsumption.toFixed(2) }}L
                    </div>

                    <div class="flex items-center space-x-1 rtl:space-x-reverse text-sm font-medium">

                        <span>Total cost: {{ currencySign }} {{
                            statisticsStore.statisticsHighlights?.totalWaxCost.toFixed(2) }}</span>
                    </div>
                </div>
            </div>
        </div>


        <!-- Statistics summary table -->

        <div v-if="statisticsStore.statisticsSummary">
            <div class="m-4 shadow-2xl pb-6">
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
                            <tr class="odd:bg-white even:bg-gray-50 border-b"
                                v-for="stationStatistics in statisticsStore.statisticsSummary?.allStationStatistics"
                                :key="stationStatistics.detergentCost">
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
                                <td class="px-6 py-3">{{ currencySign }} {{
                                    statisticsStore.statisticsSummary.totalRevenue.toFixed(2) }}
                                </td>
                                <td class="px-6 py-3">{{ currencySign }} {{
                                    statisticsStore.statisticsSummary.totalWaterCost.toFixed(2)
                                    }}</td>
                                <td class="px-6 py-3">{{ currencySign }} {{
                                    statisticsStore.statisticsSummary.totalDetergentCost.toFixed(2) }}</td>
                                <td class="px-6 py-3">{{ currencySign }} {{
                                    statisticsStore.statisticsSummary.totalWaxCost.toFixed(2) }}
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