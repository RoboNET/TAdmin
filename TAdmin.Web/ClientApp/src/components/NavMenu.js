import React, { useEffect, useState } from 'react';
import { NavItem, NavLink, Nav, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export const NavMenu = ({ databases }) => {
  const [selectedDatabases, setSelectedDatabases] = useState()
  const [dropdownOpen, setDropdownOpen] = useState(false);

  useEffect(() => {
    setSelectedDatabases(databases[0])
  }, [])

  const toggle = () => setDropdownOpen(prevState => !prevState);

  return (
    <Nav vertical>

      <Dropdown isOpen={dropdownOpen} toggle={toggle}>
        <DropdownToggle caret>
          databases
          </DropdownToggle>
        <DropdownMenu>
          {databases.map(database => {
            return <DropdownItem key={database.name} onClick={() => setSelectedDatabases(database)}>{database.name}</DropdownItem>
          })
          }
        </DropdownMenu>
      </Dropdown>
      {selectedDatabases && <div>
        {
          selectedDatabases.tables.map(table => (
            <NavItem key={`${selectedDatabases.name}/${table.name}`} >
              <NavLink tag={Link} className="text-dark" to={`/${selectedDatabases.name}/${table.name}`}>{table.name}</NavLink>
            </NavItem>))
        }
      </div>
        }
    </Nav>
  );
}
