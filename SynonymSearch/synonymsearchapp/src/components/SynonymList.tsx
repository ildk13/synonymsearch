import React from 'react';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Typography } from '@mui/material';
import useSynonymStore from '../store/synonym-store';

const SynonymList: React.FC = () => {
  const { searchResult, word } = useSynonymStore();

  return (
    <TableContainer component={Paper} sx={{ marginTop: 4 }}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>
              <Typography variant="h6">Word</Typography>
            </TableCell>
            <TableCell>
              <Typography variant="h6">Synonyms</Typography>
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {searchResult.length > 0 ? (
            <TableRow>
              <TableCell>{word}</TableCell>
              <TableCell>{searchResult.join(', ')}</TableCell>
            </TableRow>
          ) : (
            <TableRow>
              <TableCell colSpan={2} align="center">
                No synonyms found
              </TableCell>
            </TableRow>
          )}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default SynonymList;
