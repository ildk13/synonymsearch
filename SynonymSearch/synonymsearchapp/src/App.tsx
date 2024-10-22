import React from 'react';
import { Typography, Container } from '@mui/material';
import SynonymInput from './components/SynonymInput';
import SynonymList from './components/SynonymList';
import ErrorAlert from './components/ErrorAlert';


const App = () => {

  return (
    <Container maxWidth="sm" sx={{ marginTop: 5 }}>
      <Typography variant="h4" gutterBottom>
        Synonym Search Tool
      </Typography>
      
      <SynonymInput />
      
      <ErrorAlert />
      
      <SynonymList />
    </Container>
  );
}

export default App;
