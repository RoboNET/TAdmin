import React from 'react';
import { Table } from 'reactstrap';
import { Table as TableModel } from '../utils/base.types'

export const TableView = ({ table }: { table: TableModel }) => {
    return (
        <div>
            <h1>{table.name}</h1>
            <Table responsive>
                <thead>
                    <tr>
                        {table.fields.map((field: any, i: number) => (
                            <th key={i}>{field.name}</th>
                        ))}
                    </tr>
                </thead>
                <tbody></tbody>
            </Table>
        </div>)
}