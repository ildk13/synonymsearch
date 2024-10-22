import axios from 'axios'

axios.defaults.baseURL = 'http://localhost:5089/';

export const searchSynonyms = async (word: string): Promise<string[]> => {
    try {
        const response = await axios.get<string[]>(`/api/synonym?word=${word}`);
        return response.data;
    } catch (error) {
        throw new Error('Failed to fetch synonyms');
    }
} 

export const addSynonym = async (word: string, newSynonym: string) => {
    try {
        await axios.post('/api/synonym/add', {word, synonyms: [newSynonym]});
        return true;
    } catch (error) {
        throw new Error('Failed to add synonym!')
    }
};