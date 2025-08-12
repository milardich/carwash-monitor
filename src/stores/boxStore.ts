import { type Box } from '@/api/box.api'
import { defineStore } from 'pinia'

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

        setBoxStatus(boxStatus: string) {
            if (this.selectedBox) {
                this.selectedBox.status = boxStatus
            }
        }
    }
})
