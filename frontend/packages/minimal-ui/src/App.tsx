import './App.css';
import { ThemeProvider } from '../lib';
import Router from './routes/sections';

function App() {
  return (
    <ThemeProvider>
      <Router />
    </ThemeProvider>
  );
}

export default App;
