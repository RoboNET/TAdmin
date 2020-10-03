import React, { useState, useEffect } from 'react';
import { Table } from 'reactstrap';
import { Table as TableModel } from '../utils/base.types'
import { baseRequest } from '../utils/data'

const createQuery = (table: TableModel, dbName: string) => {
    return `query{
        values(dbName:"${dbName}", tableName:"${table.name}")
          {
            columns{
                name
                value
            }
          } 
      }`
}

export const TableView = ({ table, dbName }: { table: TableModel, dbName: string }) => {
    const [tableData, setTableData] = useState<any>();

    useEffect(() => {
        baseRequest(createQuery(table, dbName)).then(data => {
            setTableData(data)
        })
    }, [])

    return (
        tableData ? 
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
                <tbody>
                    
                    {tableData.values.map((value: any) => (
                        <tr>
                            {table.fields.map((field: any) => (
                                <td>{value.columns.find((column: any) => column.name === field.name).value}</td>
                            ))}
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div> : <></>)
}