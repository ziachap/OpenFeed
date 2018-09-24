import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import FetchData from './components/FetchData';
import Counter from './components/Counter';
import NewsReel from "./components/NewsReel";

export const routes = <Layout>
    <Route exact path='/home' component={Home} />
    <Route path='/' component={ NewsReel } />
    <Route path='/counter' component={ Counter } />
    <Route path='/fetchdata/:startDateIndex?' component={ FetchData } />
</Layout>;
