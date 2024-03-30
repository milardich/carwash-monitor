<script setup lang="ts">
import { Line } from 'vue-chartjs';
</script>

<template>
    <div class="rounded-2xl border-1 border-black h-64 mt-10 shadow-lg bg-white-light">
        <Line :data="data" :options="options" />
    </div>
</template>

<script lang="ts">
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
} from 'chart.js'
import * as chartConfig from '@/components/chartConfig.js'

ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
)

export default {
    name: 'App',
    components: {
        Line
    },
    data() {
        return chartConfig
    }
}
</script>