import './App.css';
import { ThemeProvider } from '../lib';
import Router from './routes/sections';
import { enqueueSnackbar } from 'notistack';

function App() {
  enqueueSnackbar('asd', {
    variant: 'error',
  });
  enqueueSnackbar('asd2', {
    variant: 'success',
  });
  enqueueSnackbar('asd3', {
    variant: 'info',
  });
  return (
    <ThemeProvider>
      <Router />
    </ThemeProvider>
  );
}

export default App;
