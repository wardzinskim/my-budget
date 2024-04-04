import './App.css';
import { ThemeProvider } from '../lib';
import Router from '../lib/routes/sections';

function App() {
  return (
    <ThemeProvider>
      <Router />
    </ThemeProvider>
  );
}

export default App;
