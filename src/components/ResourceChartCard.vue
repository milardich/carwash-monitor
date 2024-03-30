<script setup lang="ts">
import { Bar } from 'vue-chartjs';
</script>

<template>
    <div class="rounded-2xl border-1 border-black h-64 mt-10 shadow-lg bg-white-light">
        Resource consumption chart 1
        <Bar :data="data" :options="options" />
    </div>
</template>

<script lang="ts">
import {
    Chart as ChartJS,
    Title,
    Tooltip,
    Legend,
    BarElement,
    CategoryScale,
    LinearScale
} from 'chart.js'

ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend)

export default {
    name: 'App',
    components: {
        Bar
    },
    data() {
        return {
            data: {
                labels: ['January', 'February', 'March'],
                datasets: [{ data: [40, 20, 12] }]
            },
            options: {
                responsive: true
            }
        }
    }
}
</script>