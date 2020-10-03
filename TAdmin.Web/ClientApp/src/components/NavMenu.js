import React, { Component } from 'react';
import { NavItem, NavLink, Nav } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export const NavMenu = ({ databases }) => {
  return (
    <Nav vertical>
      {databases.map(database => (
        <div key={`${database.name}`} >
          <p tag={Link} className="text-dark" to="/">{database.name}</p>
          {
            database.tables.map(table => (
              <NavItem key={`${database.name}/${table.name}`} >
                <NavLink tag={Link} className="text-dark" to={`/${database.name}/${table.name}`}>{table.name}</NavLink>
              </NavItem>))
          }
        </div>
      ))}
    </Nav>
  );
}
