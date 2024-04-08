import './App.css';
import { ThemeProvider } from '@repo/minimal-ui';
import Router from './routes/router';

function App() {
  return (
    <ThemeProvider>
      <Router></Router>
    </ThemeProvider>
  );
}

export default App;
