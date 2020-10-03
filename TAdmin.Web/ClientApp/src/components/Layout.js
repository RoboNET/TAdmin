import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';
import { NavMenu } from './NavMenu';

export const Layout = ({children, databases}) => {
  return (
    <Container>
      <Row>
        <Col xs="3">
          <NavMenu databases={databases} />
        </Col>
        <Col>
          {children}
        </Col>
      </Row>
    </Container>
  );
}
