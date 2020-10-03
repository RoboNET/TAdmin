import { DbData } from './base.types'

const defaultDataQuery = `query{
    databases{
      name
      tables{
        name
        ...RelationFields
      }
    }
  }
  
  fragment RelationFields on RelationTable {
    fields{
      name
      type
      isForeignKey
    }
  }`

export const getDefaultData = async (): Promise<DbData> => {
    return await baseRequest<DbData>(defaultDataQuery);
}

export const baseRequest = async <Response>(query: string): Promise<Response> => {
    const response = await fetch('/GraphQL', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
        },
        body: JSON.stringify({query: query})
      })

      const json = await response.json()

      return json.data as Response;
}