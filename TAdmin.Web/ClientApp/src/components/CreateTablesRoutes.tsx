import React from 'react';
import { Route, Switch } from 'react-router';
import { TableView } from './TableView';

export const CreateTablesRoutes = ({ databases }: any) => {
    return (
        <Switch>
            {databases.map((database: any) =>
                database.tables.map((table: any) => <
                    Route path={`/${database.name}/${table.name}`} render={props => (
                        <TableView {...props} table={table} dbName={database.name} />)} />))}
        </Switch>
    )
}