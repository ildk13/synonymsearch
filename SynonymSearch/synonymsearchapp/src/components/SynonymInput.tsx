import React, { useState } from 'react';
import { TextField, Button, Box } from '@mui/material';
import useSynonymStore from '../store/synonym-store';

const SynonymInput: React.FC = () => {
  const { word, setWord, addSynonym, searchSynonyms } = useSynonymStore();
  const [newSynonym, setNewSynonym] = useState<string>('');

  const handleAddSynonym = () => {
    addSynonym(newSynonym);
    setNewSynonym(''); // Clear the input
  };

  return (
    <Box>
      <TextField
        label="Enter a word"
        value={word}
        onChange={(e) => setWord(e.target.value)}
        fullWidth
        margin="normal"
      />
      
      <TextField
        label="Enter a synonym"
        value={newSynonym}
        onChange={(e) => setNewSynonym(e.target.value)}
        fullWidth
        margin="normal"
      />
      
      <Button
        variant="contained"
        color="primary"
        onClick={handleAddSynonym}
        sx={{ marginTop: 2 }}
        fullWidth
      >
        Add Synonym
      </Button>
      
      <Button
        variant="outlined"
        color="secondary"
        onClick={searchSynonyms}
        sx={{ marginTop: 2 }}
        fullWidth
      >
        Search Synonyms
      </Button>
    </Box>
  );
};

export default SynonymInput;
