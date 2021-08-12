import React, { useEffect, useState } from 'react';
import { ThemeProvider as MuithemeProvider } from "@material-ui/core/styles";
import theme from './theme/theme';
import RegistrarUsuario from './componenets/seguridad/RegistrarUsuario';
import Login from './componenets/seguridad/Login';
import PerfilUsuario from './componenets/seguridad/PerfilUsuario';
import { Browser as Router, Switch, Route } from "react-router-dom";
import AppNavbar from './componenets/navegacion/AppNavbar';
import { useStateValue } from './contexto/store';
import { obtenerUsuarioActual } from './actions/UsuarioAction';

function App() {

  const [{ sesionUsuario }] = useStateValue();

  const [iniciaApp, setIniciaApp] = useState(false);

  useEffect(() => {
    if (!iniciaApp) {
      obtenerUsuarioActual(dispatch).then(response => {
        setIniciaApp(true);
      }).catch(error => {
        setIniciaApp(true);
      });
    }
  }, [iniciaApp]);

  return (
    <Router>
      <MuithemeProvider theme={theme}>
        <AppNavbar />
        <Grid>
          <Switch>
            <Route exact path="/auth/login" component={Login} />
            <Route exact path="/auth/registrar" component={RegistrarUsuario} />
            <Route exact path="/auth/perfil" component={PerfilUsuario} />
            <Route exact path="/" component={PerfilUsuario} />
          </Switch>
        </Grid>
        <PerfilUsuario />
      </MuithemeProvider>
    </Router>
  );
}

export default App;
