<script setup lang="ts">
import { Line } from 'vue-chartjs';
import { ref, watch } from 'vue';
import { resourceStore } from '@/stores/resourceStore';
import { computed } from 'vue';

const props = defineProps<{
    labels: String[],
    data: number[],
    resourceLabel: string
}>();

const chartData = computed(() => ({
    labels: props.labels,
    datasets: [
        {
            label: props.resourceLabel,
            backgroundColor: '#4B49AC', //TODO: make this a prop
            data: props.data
        }
    ]
}));

const chartOptions = ref({
    responsive: true,
    maintainAspectRatio: false
});

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
        <Line v-if="resourceStore.resourceConsumptions.length > 0" :data="chartData" :options="chartOptions" />
        <!-- <div v-else>
            Loading charts...
        </div> -->
    </div>
</template>
