import './App.css';
import { ThemeProvider } from '../lib';
import Router from './routes/sections';
import { AlertDialogProvider } from '../lib/components/alert-dialog/alert-dialog-provider';

function App() {
  return (
    <ThemeProvider>
      <AlertDialogProvider>
        <Router />
      </AlertDialogProvider>
    </ThemeProvider>
  );
}

export default App;
