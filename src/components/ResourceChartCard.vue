<script setup lang="ts">
import { Line } from 'vue-chartjs';
import { ref, watch } from 'vue';

const props = defineProps<{
    labels: String[],
    data: number[],
}>();

const chartData = ref({
    labels: props.labels,
    datasets: [
        {
            label: "Test Label",
            backgroundColor: '#4B49AC',
            data: props.data
        }
    ]
});

const chartOptions = ref({
    responsive: true,
    maintainAspectRatio: false
});

// Watch for changes in props and update chartData accordingl
watch(() => props, (newProps) => {
    chartData.value.labels = newProps.labels;
    chartData.value.datasets[0].data = newProps.data;
}, { deep: true });

</script>

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
    name: 'ResourceChartCard',
    components: {
        Line
    }
}
</script>

<template>
    <div class="rounded-2xl border-1 border-black h-64 mt-10 shadow-lg bg-white-light">
        <Line :data="chartData" :options="chartOptions" />
        {{ console.log("loaded hheehheeh" + chartData) }}
    </div>
</template>
