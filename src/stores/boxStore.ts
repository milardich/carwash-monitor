import { type Box } from '@/api/box.api'
import { defineStore } from 'pinia'
import { changeBoxStatus, createBox } from '@/api/box.api'
import { useStationStore } from './stationStore'

export const useBoxStore = defineStore('box', {
    state: () => ({
        boxPopupOpen: false,
        selectedBox: undefined as Box | undefined,
        refreshIntervalId: null as number | null
    }),

    actions: {
        setSelectedBox(box: Box) {
            this.selectedBox = box
        },

        toggleBoxPopup() {
            this.boxPopupOpen = !this.boxPopupOpen
        },

        async setBoxStatus(boxStatus: string) {
            if (!this.selectedBox) return

            this.selectedBox.status = boxStatus
            await changeBoxStatus(this.selectedBox.id, boxStatus)
            const stationStore = useStationStore()
            await stationStore.updateSelectedStation()
        },

        async createBox(stationId: string) {
            if (!stationId) return
            this.boxPopupOpen = false
            await createBox(stationId)
            const stationStore = useStationStore()
            await stationStore.updateSelectedStation()
        }
    }
})
