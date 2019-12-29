import React from "react";
import { Route, Switch } from "react-router-dom";
import AppliedRoute from "./Components/AppliedRoute";
import AuthenticatedRoute from "./Components/AuthenticatedRoute";
import UnauthenticatedRoute from "./Components/UnauthenticatedRoute";
import Home from "./Containers/Home";
import Login from "./Containers/Login";
import Setup from "./Containers/Setup";
import Projects from "./Containers/Projects";
import Project from "./Containers/Project";
import NotFound from "./Containers/NotFound";

export default ({ childProps }) =>
  <Switch>
    <AppliedRoute path="/" exact component={Home} props={childProps} />
    <UnauthenticatedRoute path="/login" exact component={Login} props={childProps} />
    <UnauthenticatedRoute path="/setup" exact component={Setup} props={childProps} />
    <AuthenticatedRoute path="/projects" exact component={Projects} props={childProps} />
    <AuthenticatedRoute path="/projects/:id" exact component={Project} props={childProps} />
    { /* Finally, catch all unmatched routes */ }
    <Route component={NotFound} />
  </Switch>;