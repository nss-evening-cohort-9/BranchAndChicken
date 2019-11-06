import React from 'react';
import logo from './logo.svg';
import './App.css';
import TrainerList from './Components/TrainerList/TrainerList'

class App extends React.Component {
  render() {
      return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
            Edit <code>src/App.js</code> and save to reload.
          </p>
          <TrainerList/>
        </header>
      </div>
    );
  }
}

export default App;
