import { defineStore } from 'pinia'
import { createWashCycle, type WashCycleRequest } from '@/api/washCycle.api'

export const useWashCycleStore = defineStore('washCycles', {
    state: () => ({}),

    actions: {
        async createWashCycle(request: WashCycleRequest, boxId: string) {
            if (!boxId) return
            return await createWashCycle(request, boxId)
        }
    },

    getters: {}
})
