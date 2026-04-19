<script lang="ts" setup>
import { useStatisticsStore } from '@/stores/statisticsStore';
import { onMounted } from 'vue';

const statisticsStore = useStatisticsStore();
const currencySign = '€'
let currentDate: Date = new Date()
const monthNames = ["January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
];

onMounted(async () => {
    statisticsStore.loadStatistics();
});
</script>

<template>
    <div class="h-full">
        <div class="p-2 sm:p-4">
            <h1 class="text-2xl sm:text-3xl font-bold">
                Statistics
                <span
                    class="block sm:inline text-lg sm:text-3xl text-gray-600 sm:text-inherit font-normal sm:font-bold">
                    ({{ monthNames[currentDate.getMonth()] }}, {{ currentDate.getFullYear() }})
                </span>
            </h1>
        </div>

        <!-- Highlights -->
        <div class="grid gap-4 lg:gap-8 grid-cols-1 sm:grid-cols-2 md:grid-cols-3 px-2 sm:px-4 pt-4 sm:pt-8">
            <div class="relative p-5 sm:p-6 rounded-2xl bg-white shadow-md">
                <div class="space-y-2">
                    <div class="text-sm font-medium text-gray-500">Total revenue</div>
                    <div class="text-2xl sm:text-3xl text-black break-words">
                        {{ currencySign }} {{ statisticsStore.statisticsHighlights?.totalRevenue.toFixed(2) }}
                    </div>
                </div>
            </div>

            <div class="relative p-5 sm:p-6 rounded-2xl bg-white shadow-md">
                <div class="space-y-2">
                    <div class="text-sm font-medium text-gray-500">Best performing station</div>
                    <div class="text-2xl sm:text-3xl text-black break-words">
                        {{ statisticsStore.statisticsHighlights?.bestStationName }}
                    </div>
                    <div class="text-sm font-medium">
                        Total revenue: {{ currencySign }}
                        {{ statisticsStore.statisticsHighlights?.bestStationRevenue.toFixed(2) }}
                    </div>
                </div>
            </div>

            <div class="relative p-5 sm:p-6 rounded-2xl bg-white shadow-md sm:col-span-2 md:col-span-1">
                <div class="space-y-2">
                    <div class="text-sm font-medium text-gray-500">Number of washes</div>
                    <div class="text-2xl sm:text-3xl text-black">
                        {{ statisticsStore.statisticsHighlights?.totalWashCount }}
                    </div>
                </div>
            </div>
        </div>

        <!-- Consumptions -->
        <div class="grid gap-4 lg:gap-8 grid-cols-1 sm:grid-cols-2 md:grid-cols-3 px-2 sm:px-4 pt-4">
            <div class="relative p-5 sm:p-6 rounded-2xl bg-white shadow-md">
                <div class="space-y-2">
                    <div class="text-sm font-medium text-gray-500">Water consumption</div>
                    <div class="text-2xl sm:text-3xl text-black">
                        {{ statisticsStore.statisticsHighlights?.totalWaterConsumption }}L
                    </div>
                    <div class="text-sm font-medium">
                        Total cost: {{ currencySign }}
                        {{ statisticsStore.statisticsHighlights?.totalWaterCost.toFixed(2) }}
                    </div>
                </div>
            </div>

            <div class="relative p-5 sm:p-6 rounded-2xl bg-white shadow-md">
                <div class="space-y-2">
                    <div class="text-sm font-medium text-gray-500">Detergent consumption</div>
                    <div class="text-2xl sm:text-3xl text-black">
                        {{ statisticsStore.statisticsHighlights?.totalDetergentConsumption }}L
                    </div>
                    <div class="text-sm font-medium">
                        Total cost: {{ currencySign }}
                        {{ statisticsStore.statisticsHighlights?.totalDetergentCost.toFixed(2) }}
                    </div>
                </div>
            </div>

            <div class="relative p-5 sm:p-6 rounded-2xl bg-white shadow-md sm:col-span-2 md:col-span-1">
                <div class="space-y-2">
                    <div class="text-sm font-medium text-gray-500">Wax consumption</div>
                    <div class="text-2xl sm:text-3xl text-black">
                        {{ statisticsStore.statisticsHighlights?.totalWaxConsumption.toFixed(2) }}L
                    </div>
                    <div class="text-sm font-medium">
                        Total cost: {{ currencySign }}
                        {{ statisticsStore.statisticsHighlights?.totalWaxCost.toFixed(2) }}
                    </div>
                </div>
            </div>
        </div>

        <!-- Statistics summary table -->
        <div v-if="statisticsStore.statisticsSummary" class="mx-2 sm:mx-4 mt-4 sm:mt-6 shadow-md pb-6 rounded-lg">
            <div class="overflow-x-auto sm:rounded-lg">
                <table class="w-full text-sm text-left rtl:text-right text-gray-500 whitespace-nowrap">
                    <thead class="text-xs text-gray-700 uppercase bg-gray-50">
                        <tr>
                            <th scope="col" class="px-4 sm:px-6 py-3">Station name</th>
                            <th scope="col" class="px-4 sm:px-6 py-3">Total revenue</th>
                            <th scope="col" class="px-4 sm:px-6 py-3">Water cost</th>
                            <th scope="col" class="px-4 sm:px-6 py-3">Detergent cost</th>
                            <th scope="col" class="px-4 sm:px-6 py-3">Wax cost</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="odd:bg-white even:bg-gray-50 border-b"
                            v-for="stationStatistics in statisticsStore.statisticsSummary?.allStationStatistics"
                            :key="stationStatistics.detergentCost">
                            <td class="px-4 sm:px-6 py-4">{{ stationStatistics.stationName }}</td>
                            <td class="px-4 sm:px-6 py-4">
                                {{ currencySign }} {{ stationStatistics.revenue.toFixed(2) }}
                            </td>
                            <td class="px-4 sm:px-6 py-4">
                                {{ currencySign }} {{ stationStatistics.waterCost.toFixed(2) }}
                            </td>
                            <td class="px-4 sm:px-6 py-4">
                                {{ currencySign }} {{ stationStatistics.detergentCost.toFixed(2) }}
                            </td>
                            <td class="px-4 sm:px-6 py-4">
                                {{ currencySign }} {{ stationStatistics.waxCost.toFixed(2) }}
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr class="font-semibold table-footer">
                            <th scope="row" class="px-4 sm:px-6 py-3 text-base">Total</th>
                            <td class="px-4 sm:px-6 py-3">
                                {{ currencySign }} {{ statisticsStore.statisticsSummary.totalRevenue.toFixed(2) }}
                            </td>
                            <td class="px-4 sm:px-6 py-3">
                                {{ currencySign }} {{ statisticsStore.statisticsSummary.totalWaterCost.toFixed(2) }}
                            </td>
                            <td class="px-4 sm:px-6 py-3">
                                {{ currencySign }} {{ statisticsStore.statisticsSummary.totalDetergentCost.toFixed(2) }}
                            </td>
                            <td class="px-4 sm:px-6 py-3">
                                {{ currencySign }} {{ statisticsStore.statisticsSummary.totalWaxCost.toFixed(2) }}
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
        <div v-else class="px-2 sm:px-4 mt-4 text-gray-500">
            Loading statistics summary table...
        </div>
    </div>
</template>
