import './App.css';
import { ThemeProvider } from '@repo/minimal-ui';
import Router from './routes/sections';

function App() {
  return (
    <ThemeProvider>
      <Router></Router>
    </ThemeProvider>
  );
}

export default App;
