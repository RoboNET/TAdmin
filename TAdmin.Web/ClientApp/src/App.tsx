import React, { useEffect, useState } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { getDefaultData } from './utils/data'
import { DbData } from './utils/base.types'
import { CreateTablesRoutes } from './components/CreateTablesRoutes'

import './custom.css'

const App = () => {
  const [dbData, setTest] = useState<DbData>()

  useEffect( () => {
    getDefaultData().then((resp) => {
      setTest(resp);
    }) 
  },[])

  return (dbData ?
    <Layout databases={dbData?.databases}>
      <Route exact path='/' component={Home} />
        <CreateTablesRoutes databases={dbData?.databases} />
    </Layout>
    : <></>
  );
}

export default App;
