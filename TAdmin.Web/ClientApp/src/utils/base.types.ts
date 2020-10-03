export type Table = {
    name: string;
    fields: any[]
}

export type Database = {
    name: string;
    tables: Table[];
}

export type DbData = {
    databases: Database[]
}