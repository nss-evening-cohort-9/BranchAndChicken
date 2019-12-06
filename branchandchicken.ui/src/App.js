import React from 'react';
import firebase from 'firebase';
import {Route, BrowserRouter, Redirect, Switch}  from 'react-router-dom';

import TrainerList from './Components/TrainerList/TrainerList'
import Login from './Components/Login/Login'
import Navbar from './Components/Navbar/Navbar'
import Register from './Components/Register/Register'
import fbConnection from './Requests/connection';


import './App.css';

fbConnection();


const PrivateRoute = ({ component: Component, authenticated, ...rest}) => {
  return (
    <Route
      {...rest}
      render={props =>
        authenticated === true ? (
          <Component {...props} />
        ) : (
          <Redirect
            to={{ pathname: '/login', state: {from: props.location}}}
          />
        )
      }
    />
  );
};

const PublicRoute = ({ component: Component, authenticated, ...rest}) => {
  return (
    <Route
      {...rest}
      render={props =>
        authenticated === false ? (
          <Component {...props} />
        ) : (
          <Redirect
            to={{ pathname: '/trainers', state: {from: props.location}}}
          />
        )
      }
    />
  );
};

class App extends React.Component {

  state={
    authenticated: false,
  }

  componentDidMount () {
    this.removeListener = firebase.auth().onAuthStateChanged((user) => {
      if (user) {
        this.setState({authenticated: true});
      } else {
        this.setState({authenticated: false});
      }
    });
  }

  componentWillUnmount () {
    this.removeListener();
  }

  logout = () => {
    this.setState({authenticated: false});
  }

  render() {
      return (
        <BrowserRouter>
        <div>
          <Navbar
            authenticated={this.state.authenticated}
            runAway={this.logout}
          />
          <div className="container">
            <div className="row">
              <Switch>
                <PublicRoute
                  path="/register"
                  authenticated={this.state.authenticated}
                  component={Register}
                />
                <PublicRoute
                  path="/login"
                  authenticated={this.state.authenticated}
                  component={Login}
                />
                <PrivateRoute
                  path="/trainers"
                  authenticated={this.state.authenticated}
                  component={TrainerList}
                />
              </Switch>
            </div>
          </div>
        </div>
      </BrowserRouter>
    );
  }
}

export default App;
