import { create} from 'zustand';
import { addSynonym, searchSynonyms } from '../api/api-client';

interface SynonymState {
    word: string;
    synonyms:string[];
    searchResult:string[];
    error: string | null;

    setWord: (newWord: string) => void;
    setNewSynonym: (newSynonym: string) => void;
    addSynonym: (newSynonym: string) => Promise<void>;
    searchSynonyms: () => Promise<void>;
}

const useSynonymStore = create<SynonymState>((set) => ({
    word: '',
    synonyms: [],
    searchResult: [],
    error: null,
    
    setWord: (newWord:string) => set({ word: newWord }),
    setNewSynonym: (newSynonym:string) => set((state:SynonymState) => ({
      synonyms: [...state.synonyms, newSynonym]
})),

searchSynonyms: async () => {
    try {
      const word = useSynonymStore.getState().word;
      const results = await searchSynonyms(word);
      set({ searchResult: results, error: null });
    } catch (error: any) {
      set({ error: error.message });
    }
  },

addSynonym: async (newSynonym: string) => {
    try {
      const word = useSynonymStore.getState().word;
      await addSynonym(word, newSynonym);
      set((state:SynonymState) => ({
        synonyms: [...state.synonyms, newSynonym],
        error: null,
      }));
    } catch (error: any) {
      set({ error: error.message });
    }
  },
}));

export default useSynonymStore;