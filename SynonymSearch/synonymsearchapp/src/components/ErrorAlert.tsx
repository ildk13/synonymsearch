import React from 'react';
import { Alert } from '@mui/material';
import useSynonymStore from '../store/synonym-store';

const ErrorAlert: React.FC = () => {
  const { error } = useSynonymStore();

  return (
    <>
      {error && <Alert severity="error" sx={{ marginTop: 2 }}>{error}</Alert>}
    </>
  );
};

export default ErrorAlert;
