<script setup lang="ts">
import { useBoxStore } from '@/stores/boxStore';
import { useStationStore } from '@/stores/stationStore';
import { useWashCycleStore } from '@/stores/washCycleStore';
import { ref, onMounted } from 'vue';
import { type Box } from '@/api/box.api';
import type { WashCycleRequest } from '@/api/washCycle.api';

const stationStore = useStationStore()
const boxStore = useBoxStore()
const washCycleStore = useWashCycleStore()


// STATION
const newStationName = ref('')
const handleCreateStation = async () => {
    if (!newStationName.value.trim()) {
        alert('Please enter a station name!')
        return
    }

    await stationStore.createStation(newStationName.value.trim())
    alert('Station created successfully!')
    await stationStore.loadStations()
    newStationName.value = ''
}


// MANAGE STATIONS
const editingStationId = ref('')
const editingStationName = ref('')

const startEditStation = (id: string, name: string) => {
    editingStationId.value = id
    editingStationName.value = name
}

const cancelEditStation = () => {
    editingStationId.value = ''
    editingStationName.value = ''
}

const handleUpdateStation = async () => {
    if (!editingStationName.value.trim()) {
        alert('Please enter a station name!')
        return
    }

    await stationStore.updateStation(editingStationId.value, editingStationName.value.trim())
    cancelEditStation()
}

const handleDeleteStation = async (id: string, name: string) => {
    if (!confirm(`Delete station "${name}" and all its boxes?`)) return
    await stationStore.deleteStation(id)
}

const handleAddBox = async (stationId: string) => {
    await boxStore.createBox(stationId)
    await stationStore.loadStations()
}

const handleDeleteBox = async (box: Box) => {
    if (box.status !== 'INACTIVE' && box.status !== 'MAINTENANCE') {
        alert('Only INACTIVE or MAINTENANCE boxes can be deleted.')
        return
    }
    if (!confirm(`Delete Box #${box.number}?`)) return
    await boxStore.deleteBox(box.id)
}


// WASHCYCLE
const washCycleSelectedStationId = ref('')
const selectedBoxId = ref('')
const stationBoxes = ref<Box[]>([])
const coinAmount = ref(0)
const waterConsumption = ref(0)
const detergentConsumption = ref(0)
const waxConsumption = ref(0)

const updateBoxesOnDropdownChange = () => {
    const station = stationStore.stations.find(s => s.id === washCycleSelectedStationId.value)
    const filteredBoxes = station?.boxes.filter(b => b.status === 'AVAILABLE') || []
    stationBoxes.value = filteredBoxes
    selectedBoxId.value = stationBoxes.value.length ? stationBoxes.value[0].id : ''
}

const handleCreateWashCycle = async () => {
    if (!selectedBoxId.value) {
        alert('Please select a box!')
        return
    }

    const request: WashCycleRequest = {
        coinAmount: coinAmount.value,
        waterConsumption: waterConsumption.value,
        detergentConsumption: detergentConsumption.value,
        waxConsumption: waxConsumption.value
    }

    await washCycleStore.createWashCycle(request, selectedBoxId.value)
    alert('Wash Cycle created successfully!')

    coinAmount.value = 0
    waterConsumption.value = 0
    detergentConsumption.value = 0
    waxConsumption.value = 0
}


// SIMULATED WASHCYCLE
const simulatedStationId = ref<string>("")
const isSimulating = ref(false)

const handleSimulatedWashCycle = async () => {
    if (!simulatedStationId.value) {
        alert("Please select a station")
        return
    }

    const station = stationStore.stations.find(s => s.id === simulatedStationId.value)
    if (!station) return

    const availableBoxes = station.boxes.filter(b => b.status === "AVAILABLE")
    if (availableBoxes.length === 0) {
        alert("No available boxes in this station")
        return
    }

    const randomBox = availableBoxes[Math.floor(Math.random() * availableBoxes.length)]

    await boxStore.setBoxStatus(randomBox.id, "IN_USE")

    isSimulating.value = true

    setTimeout(async () => {
        const request = {
            coinAmount: Math.floor(Math.random() * 10) + 1,
            waterConsumption: Math.floor(Math.random() * 50) + 10,
            detergentConsumption: Math.floor(Math.random() * 20) + 5,
            waxConsumption: Math.floor(Math.random() * 10) + 2
        }

        await boxStore.setBoxStatus(randomBox.id, "AVAILABLE")
        await washCycleStore.createWashCycle(request, randomBox.id)

        isSimulating.value = false
    }, 10_000)
}

onMounted(async () => {
    await stationStore.loadStations()
    if (stationStore.stations.length) {
        washCycleSelectedStationId.value = stationStore.stations[0].id
        updateBoxesOnDropdownChange()
    }
})
</script>

<template>
    <div class="p-2 sm:p-4">
        <h1 class="text-2xl sm:text-3xl font-bold mb-4 sm:mb-6">Control Panel</h1>

        <div class="grid gap-4 grid-cols-1 sm:grid-cols-2 xl:grid-cols-3">

            <!-- CREATE STATION -->
            <div class="p-5 sm:p-6 border rounded-lg shadow-md bg-white">
                <h2 class="text-xl font-semibold mb-4">Create Station</h2>
                <div class="flex flex-col space-y-3">
                    <input v-model="newStationName" type="text" placeholder="Station Name"
                        class="border rounded p-2 w-full focus:outline-none focus:ring-2 focus:ring-indigo-500" />
                    <button @click="handleCreateStation"
                        class="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 w-full transition-colors">
                        Create Station
                    </button>
                </div>
            </div>

            <!-- CREATE WASHCYCLE -->
            <div class="p-5 sm:p-6 border rounded-lg shadow-md bg-white">
                <h2 class="text-xl font-semibold mb-4">Create Wash Cycle</h2>
                <div class="flex flex-col space-y-3">
                    <select v-model="washCycleSelectedStationId" @change="updateBoxesOnDropdownChange"
                        class="border rounded p-2 w-full focus:outline-none focus:ring-2 focus:ring-indigo-500">
                        <option v-for="station in stationStore.stations" :key="station.id" :value="station.id">
                            {{ station.name }}
                        </option>
                    </select>

                    <select v-model="selectedBoxId"
                        class="border rounded p-2 w-full focus:outline-none focus:ring-2 focus:ring-indigo-500">
                        <option v-for="box in stationBoxes" :key="box.id" :value="box.id">
                            Box #{{ box.number }}
                        </option>
                    </select>

                    <input v-model.number="coinAmount" type="number" placeholder="Coin Amount"
                        class="border rounded p-2 w-full focus:outline-none focus:ring-2 focus:ring-indigo-500" />
                    <input v-model.number="waterConsumption" type="number" placeholder="Water Consumption"
                        class="border rounded p-2 w-full focus:outline-none focus:ring-2 focus:ring-indigo-500" />
                    <input v-model.number="detergentConsumption" type="number" placeholder="Detergent Consumption"
                        class="border rounded p-2 w-full focus:outline-none focus:ring-2 focus:ring-indigo-500" />
                    <input v-model.number="waxConsumption" type="number" placeholder="Wax Consumption"
                        class="border rounded p-2 w-full focus:outline-none focus:ring-2 focus:ring-indigo-500" />

                    <button @click="handleCreateWashCycle"
                        class="bg-purple-500 text-white px-4 py-2 rounded hover:bg-purple-600 w-full transition-colors">
                        Create Wash Cycle
                    </button>
                </div>
            </div>

            <!-- SIMULATED WASHCYCLE -->
            <div class="p-5 sm:p-6 border rounded-lg shadow-md bg-white">
                <h2 class="text-xl font-semibold mb-4">Simulated Wash Cycle</h2>
                <div class="flex flex-col space-y-3">
                    <select v-model="simulatedStationId"
                        class="border rounded p-2 w-full focus:outline-none focus:ring-2 focus:ring-indigo-500">
                        <option v-for="station in stationStore.stations" :key="station.id" :value="station.id">
                            {{ station.name }}
                        </option>
                    </select>

                    <button @click="handleSimulatedWashCycle"
                        class="bg-orange-500 text-white px-4 py-2 rounded hover:bg-orange-600 w-full transition-colors disabled:opacity-60 disabled:cursor-not-allowed"
                        :disabled="isSimulating">
                        {{ isSimulating ? 'Simulating...' : 'Start Simulation' }}
                    </button>
                </div>
            </div>

        </div>

        <!-- MANAGE STATIONS -->
        <h2 class="text-xl sm:text-2xl font-bold mt-8 mb-4">Manage Stations</h2>
        <div class="grid gap-4 grid-cols-1 sm:grid-cols-2 xl:grid-cols-3">
            <div v-for="station in stationStore.stations" :key="station.id"
                class="p-5 sm:p-6 border rounded-lg shadow-md bg-white">
                <div class="flex items-center gap-2 mb-4">
                    <template v-if="editingStationId === station.id">
                        <input v-model="editingStationName" type="text"
                            class="border rounded p-2 flex-1 focus:outline-none focus:ring-2 focus:ring-indigo-500" />
                        <button @click="handleUpdateStation"
                            class="bg-green-500 text-white px-3 py-2 rounded hover:bg-green-600 transition-colors">
                            Save
                        </button>
                        <button @click="cancelEditStation"
                            class="bg-gray-300 text-gray-800 px-3 py-2 rounded hover:bg-gray-400 transition-colors">
                            Cancel
                        </button>
                    </template>
                    <template v-else>
                        <h3 class="text-lg font-semibold flex-1 truncate">{{ station.name }}</h3>
                        <button @click="startEditStation(station.id, station.name)"
                            class="bg-blue-500 text-white px-3 py-2 rounded hover:bg-blue-600 transition-colors">
                            Edit
                        </button>
                        <button @click="handleDeleteStation(station.id, station.name)"
                            class="bg-red-500 text-white px-3 py-2 rounded hover:bg-red-600 transition-colors">
                            Delete
                        </button>
                    </template>
                </div>

                <ul class="divide-y border rounded mb-3">
                    <li v-for="box in station.boxes" :key="box.id" class="flex items-center gap-2 p-2">
                        <span class="font-medium">Box #{{ box.number }}</span>
                        <span class="text-xs text-gray-500">{{ box.status }}</span>
                        <button @click="handleDeleteBox(box)"
                            :disabled="box.status !== 'INACTIVE' && box.status !== 'MAINTENANCE'"
                            class="ml-auto bg-red-500 text-white px-2 py-1 rounded hover:bg-red-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed">
                            Delete
                        </button>
                    </li>
                    <li v-if="!station.boxes.length" class="p-2 text-sm text-gray-500">No boxes yet.</li>
                </ul>

                <button @click="handleAddBox(station.id)"
                    class="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 w-full transition-colors">
                    Add Box
                </button>
            </div>

            <div v-if="!stationStore.stations.length" class="text-gray-500">No stations yet.</div>
        </div>
    </div>
</template>
